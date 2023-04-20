#pragma once

#include <string>
#include <vector>

namespace base
{
	class __declspec(dllexport) dynamodb_base
	{
	public:

		dynamodb_base() {};

		~dynamodb_base();

		std::vector<std::string> ListTables();

		std::vector<std::string> GetItems(const std::string tableName,
			const std::string partitionKey,
			const std::string partitionValue);

		int CreateDynamoDBConnection();

	};
}