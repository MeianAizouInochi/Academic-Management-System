//DynamoDb base headerFile
#include "dynamodb_base.h"
#include <string>
#include <vector>
#include <map>

//DYNAMO DB HEADER FILES.
#include <aws/dynamodb/DynamoDBClient.h>
#include <aws/dynamodb/model/ListTablesRequest.h>
#include <aws/dynamodb/model/ListTablesResult.h>
#include <aws/dynamodb/model/AttributeDefinition.h>
#include <aws/dynamodb/model/GetItemRequest.h>
#include <aws/dynamodb/model/ScanRequest.h>

//CREATING HEADER FILES FOR CREATING TABLE.
#include <aws/dynamodb/model/CreateTableRequest.h>
#include <aws/dynamodb/model/KeySchemaElement.h>
#include <aws/dynamodb/model/ProvisionedThroughput.h>
#include <aws/dynamodb/model/ScalarAttributeType.h>

//HEADER FILES FOR TRANSACTIONS
#include <aws/dynamodb/model/TransactWriteItem.h>
#include <aws/dynamodb/model/TransactWriteItemsRequest.h>

namespace base
{

    std::shared_ptr<Aws::DynamoDB::DynamoDBClient> DynamoDB_clientRef;

    int dynamodb_base::CreateDynamoDBConnection()
    {
        Aws::Client::ClientConfiguration config;
        config.region = Aws::Region::AP_SOUTH_1;
        try
        {
            auto DynamoDB_client = Aws::MakeShared<Aws::DynamoDB::DynamoDBClient>("dynamoDBclient", config);
            DynamoDB_clientRef = DynamoDB_client;
        }
        catch (...)
        {
            return 0;
        }
        return 1;
    }

    dynamodb_base::~dynamodb_base()
    {
        DynamoDB_clientRef = nullptr;
    }

    std::string AWSDynamoDbResultParser(Aws::DynamoDB::Model::ValueType obj, Aws::DynamoDB::Model::AttributeValue val)
    {
        //type of enums NUMBER, BYTEBUFFER, STRING_SET, NUMBER_SET, BYTEBUFFER_SET, ATTRIBUTE_MAP, ATTRIBUTE_LIST, BOOL, NULLVALUE
        switch (obj)
        {
        case Aws::DynamoDB::Model::ValueType::STRING:
            return val.GetS();
            break;
        case Aws::DynamoDB::Model::ValueType::NUMBER:
            return val.GetN();
            break;
        case Aws::DynamoDB::Model::ValueType::BOOL:
            return val.GetB() == 1 ? "TRUE" : "FALSE";
            break;
        case Aws::DynamoDB::Model::ValueType::NULLVALUE:
            return "";
            break;
        default:
            return "error";
        }
    }

    std::map<std::string, std::string>* dynamodb_base::GetItems(const std::string tableName,
        const std::string partitionKey,
        const std::string partitionValue) {
        Aws::DynamoDB::Model::GetItemRequest request;

        //base map
        std::map<std::string, std::string>* map_base = new std::map<std::string, std::string>;

        // Set up the request.
        request.SetTableName(tableName);
        request.AddKey(partitionKey,
            Aws::DynamoDB::Model::AttributeValue().SetN(partitionValue));

        // Retrieve the item's fields and values.
        const Aws::DynamoDB::Model::GetItemOutcome& outcome = DynamoDB_clientRef->GetItem(request);
        if (outcome.IsSuccess()) {

            // Reference the retrieved fields/values.
            const Aws::Map<Aws::String, Aws::DynamoDB::Model::AttributeValue>& item = outcome.GetResult().GetItem();
            if (!item.empty()) {
                map_base->insert(std::make_pair("ExecCode", "1"));
                // Output each retrieved field and its value.
                for (const auto& i : item)
                {
                    map_base->insert(std::make_pair(i.first, AWSDynamoDbResultParser(i.second.GetType(), i.second)));
                }
                map_base->insert(std::make_pair("ErrorMessage", ""));
            }
            else {
                map_base->insert(std::make_pair("ExecCode", "-1"));
                map_base->insert(std::make_pair("ErrorMessage", "User Not found , try contacting your CC"));
                return map_base;
            }
        }
        else {
            map_base->insert(std::make_pair("ExecCode", "0"));
            map_base->insert(std::make_pair("ErrorMessage", "Connectivity error , please try again later"));
            return map_base;
        }

        return map_base;
    }


    std::vector<std::string> dynamodb_base::ListTables()
    {

        std::vector<std::string> result;

        Aws::DynamoDB::Model::ListTablesRequest listTablesRequest;

        listTablesRequest.SetLimit(50);

        do {
            const Aws::DynamoDB::Model::ListTablesOutcome& outcome = DynamoDB_clientRef->ListTables(listTablesRequest);

            if (!outcome.IsSuccess())
            {
                return {outcome.GetError().GetMessage()};
;           }

            for (const auto& tableName : outcome.GetResult().GetTableNames())
                result.push_back(tableName);

            listTablesRequest.SetExclusiveStartTableName(outcome.GetResult().GetLastEvaluatedTableName());

        } while (!listTablesRequest.GetExclusiveStartTableName().empty());

        return result;
    }

    std::vector<std::string> dynamodb_base::ScanTable(const std::string tableName,const std::string projectionExpression)
    {

        Aws::DynamoDB::Model::ScanRequest request;

        std::vector<std::string> result;
        result.push_back("");
        result.push_back("");

        request.SetTableName(tableName);

        if (!projectionExpression.empty())
            request.SetProjectionExpression(projectionExpression);

        // Perform scan on table.
        const Aws::DynamoDB::Model::ScanOutcome& outcome = DynamoDB_clientRef->Scan(request);

        if (outcome.IsSuccess()) 
        {
            result[0] = "1";
            // Reference the retrieved items.
            const Aws::Vector<Aws::Map<Aws::String, Aws::DynamoDB::Model::AttributeValue>>& items = outcome.GetResult().GetItems();

            if (!items.empty()) 
            {
                
                // Iterate each item and print.
                for (const Aws::Map<Aws::String, Aws::DynamoDB::Model::AttributeValue>& itemMap : items) 
                {

                    // Output each retrieved field and its value.
                    for (const auto& itemEntry : itemMap)
                    result.push_back(AWSDynamoDbResultParser(itemEntry.second.GetType(), itemEntry.second));

                }
            }

            else {
                result[0] = "0";
                result[1] = "No item found in table";
            }
        }
        else {
            result[0] = "0";
            result[1] = outcome.GetError().GetMessage();
        }

        return result;
    }

    //SPECALIZED FUNCTION - TO Update attendance
    std::vector<std::string> dynamodb_base::UpdateAttendance(const std::string tableName,
        const std::string partitionKey,
        std::vector<std::string> partitionValues,
        const std::string ColumnName,
        const std::string SubjectName,
        const std::string dayValue,
        const std::string TotalValue)
    {

        std::vector<std::string> res;
        res.push_back("");
        res.push_back("");
        
        // Creating a vector to store multiple transaction if any
        std::vector<Aws::DynamoDB::Model::TransactWriteItem> writeItems;


        for (auto partitionValue : partitionValues)
        {
            Aws::DynamoDB::Model::Update updateRequest;
            updateRequest.WithTableName(tableName)
                .WithKey({ { partitionKey, Aws::DynamoDB::Model::AttributeValue().SetS(partitionValue) } })
                .WithUpdateExpression("SET #map.#nestedAttr.#attendanceday = #map.#nestedAttr.#attendanceday + :val1, #map.#nestedAttr.#attendanceTotal = #map.#nestedAttr.#attendanceTotal + :val2")
                .WithExpressionAttributeNames({ { "#map", ColumnName }, { "#nestedAttr", SubjectName }, { "#attendanceday", "0" }, { "#attendanceTotal", "1" } })
                .WithExpressionAttributeValues({ { ":val1", Aws::DynamoDB::Model::AttributeValue().SetN(dayValue) },{ ":val2", Aws::DynamoDB::Model::AttributeValue().SetN(TotalValue) } });
        
            Aws::DynamoDB::Model::TransactWriteItem transactionWriteItem;
            transactionWriteItem.WithUpdate(updateRequest);

            writeItems.push_back(transactionWriteItem);
        }


        // Create the transaction write items request
        Aws::DynamoDB::Model::TransactWriteItemsRequest transactionWriteItemsRequest;
        transactionWriteItemsRequest.WithTransactItems({ writeItems });

        // Execute the transaction
        auto outcome = DynamoDB_clientRef->TransactWriteItems(transactionWriteItemsRequest);

        if (outcome.IsSuccess()) {
            res[0] = "1";
            res[1] = "";
        }
        else {
            res[0] = "0";
            res[1] = outcome.GetError().GetMessage();
        }

        return res;
    }
}