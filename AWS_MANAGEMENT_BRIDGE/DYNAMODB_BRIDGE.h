#pragma once

#include "AWS_MANAGEMENT_BRIDGE.h"

#include "../AWS_MANAGEMENT_BASE/core.h"

using namespace System;
using namespace System::Collections::Generic;

namespace AWS_MANAGEMENT_BRIDGE
{
	public ref class DYNAMODB_BRIDGE : public ManagedObject<base::dynamodb_base>
	{
	public:
		DYNAMODB_BRIDGE();
		int CreateDynamoDBConnection();
		array<String^>^ ListTables();
		Dictionary<String^,String^>^ GetItems(String^ tablename, String^ partitionkey, String^ partitionvalue);
		array<String^>^ ScanTable(String^ tablename, String^ ProjectionExpression);
		/// <summary>
		/// this function updates attendance of a subject of students in dynamodb 
		/// </summary>
		/// <param name="tablename"></param>
		/// <param name="partitionkey"></param>
		/// <param name="partitionvalues"></param>
		/// <param name="ColumnName"></param>
		/// <param name="SubjectName"></param>
		/// <param name="dayvalue"></param>
		/// <param name="totalvalue"></param>
		/// <returns> it returns a array of strings where the first index(0th index) is 0 or 1 (0 - failed) (1 - successful)
		///													second index(1th index) - error if any
		/// </returns>
		array<String^>^ UpdateAttendance(String^ tablename, String^ partitionkey, array<String^>^ partitionvalues, String^ ColumnName, String^ SubjectName, String^ dayvalue, String^ totalvalue);
		void CloseConnection();
	};
}