#pragma once

#include <string>
#include <vector>
#include <map>

namespace base
{
	class __declspec(dllexport) dynamodb_base
	{
	public:

		dynamodb_base() {};

		~dynamodb_base();

		std::vector<std::string> ListTables();

		std::map<std::string, std::string>* GetItems(const std::string tableName,
			const std::string partitionKey,
			const std::string partitionValue);

		std::vector<std::string> ScanTable(const std::string tableName,const std::string projectionExpression);

		std::vector<std::string> UpdateAttendance(const std::string tableName,
			const std::string partitionKey,
			std::vector<std::string> partitionValues,
			const std::string ColumnName,
			const std::string SubjectName,
			const std::string dayValue,
			const std::string TotalValue);

		int CreateDynamoDBConnection();

	};
}