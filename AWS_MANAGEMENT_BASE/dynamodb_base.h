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

		int CreateDynamoDBConnection();

	};
}