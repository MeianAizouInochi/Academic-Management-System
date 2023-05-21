#include "DYNAMODB_BRIDGE.h"
#include <vector>

using namespace System::Runtime::InteropServices;

namespace AWS_MANAGEMENT_BRIDGE
{
	DYNAMODB_BRIDGE::DYNAMODB_BRIDGE() : ManagedObject(new base::dynamodb_base()){ }

	int DYNAMODB_BRIDGE::CreateDynamoDBConnection()
	{
	
		int value = M_Instance->CreateDynamoDBConnection();

		return value;

	}

	Dictionary<String^, String^>^ DYNAMODB_BRIDGE::GetItems(String^ tablename, String^ partitionkey, String^ partitionvalue)
	{
		std::string table_name = (const char*)(Marshal::StringToHGlobalAnsi(tablename)).ToPointer();
		std::string partition_key = (const char*)(Marshal::StringToHGlobalAnsi(partitionkey)).ToPointer();
		std::string partition_value = (const char*)(Marshal::StringToHGlobalAnsi(partitionvalue)).ToPointer();
												
		std::map<std::string,std::string>* Temp_map = M_Instance->GetItems(table_name, partition_key, partition_value);

		auto result = gcnew Dictionary<String^, String^>();


		for (auto it = Temp_map->begin(); it != Temp_map->end(); ++it)
		{
			result->Add(gcnew String(it->first.c_str()), gcnew String(it->second.c_str()));
		}

		delete Temp_map;

		return result;
	}

	array<String^>^ DYNAMODB_BRIDGE::ListTables()
	{
		std::vector<std::string> Temp_vector = M_Instance->ListTables();

		int size = Temp_vector.size();

		array<String^>^ Array_Of_Objects = gcnew array<String^>(size);


		for (int i = 0; i < size; i++)
		{
			Array_Of_Objects[i] = gcnew String(Temp_vector[i].c_str());
		}

		return Array_Of_Objects;
	}

	array<String^>^ DYNAMODB_BRIDGE::ScanTable(String^ tablename, String^ ProjectionExpression)
	{

		std::string table_name = (const char*)(Marshal::StringToHGlobalAnsi(tablename)).ToPointer();
		std::string Projection_Expression = (const char*)(Marshal::StringToHGlobalAnsi(ProjectionExpression)).ToPointer();

		std::vector<std::string> result = M_Instance->ScanTable(table_name,Projection_Expression);

		int size = result.size();

		array<String^>^ Array_Of_Objects = gcnew array<String^>(size);


		for (int i = 0; i < size; i++)
		{
			Array_Of_Objects[i] = gcnew String(result[i].c_str());
		}

		return Array_Of_Objects;
	}

	array<String^>^ DYNAMODB_BRIDGE::UpdateAttendance(String^ tablename, String^ partitionkey, array<String^>^ partitionvalues, String^ ColumnName, String^ SubjectName, String^ dayvalue, String^ totalvalue)
	{
		std::string table_name = (const char*)(Marshal::StringToHGlobalAnsi(tablename)).ToPointer();
		std::string partition_key = (const char*)(Marshal::StringToHGlobalAnsi(partitionkey)).ToPointer();
		std::string map_Column_Name = (const char*)(Marshal::StringToHGlobalAnsi(ColumnName)).ToPointer();
		std::string Subject_Name = (const char*)(Marshal::StringToHGlobalAnsi(SubjectName)).ToPointer();
		std::string day_value = (const char*)(Marshal::StringToHGlobalAnsi(dayvalue)).ToPointer();
		std::string total_value = (const char*)(Marshal::StringToHGlobalAnsi(totalvalue)).ToPointer();

		std::vector<std::string> partition_values(partitionvalues->Length,"");

		for (int i = 0; i < partitionvalues->Length; i++)
		{
			partition_values[i] = (const char*)(Marshal::StringToHGlobalAnsi(partitionvalues[i])).ToPointer();;
		}

		std::vector<std::string> result = M_Instance->UpdateAttendance(table_name, partition_key, partition_values, map_Column_Name,Subject_Name, day_value, total_value);

		int size = result.size();

		array<String^>^ Array_Of_Objects = gcnew array<String^>(size);


		for (int i = 0; i < size; i++)
		{
			Array_Of_Objects[i] = gcnew String(result[i].c_str());
		}

		return Array_Of_Objects;
	}

	void DYNAMODB_BRIDGE::CloseConnection()
	{
		M_Instance->~dynamodb_base();
		M_Instance = nullptr;
	}
}