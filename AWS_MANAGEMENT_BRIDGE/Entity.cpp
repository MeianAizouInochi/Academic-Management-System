#include "Entity.h"
#include <vector>

namespace AWS_MANAGEMENT_BRIDGE
{
	Entity::Entity() : ManagedObject(new core::AwsConnector())
	{
		Console::WriteLine("Initializing AWS SDK");
	}

	int Entity::CreateS3Connection()
	{
		Console::WriteLine("Creating S3 Connection");
		int val = M_Instance->CreateS3Connection();
		return val;
	}

	//array<String^>^ is a array of .Net String
	array<String^>^ Entity::ListObjectsS3()
	{
		std::vector<std::string> vec = M_Instance->ListObjects();

		//here creating a array of .net strings 
	
		array<String^>^ arr = gcnew array<String^>(vec.size());//The gcnew keyword is used to allocate a new managed object on the .NET heap.

		for (int i = 0; i < vec.size(); i++)
		{
			/*.c_str(): This returns a pointer to a null-terminated array of characters representing the contents of the std::string.
			* gcnew is same a new but as for managed languages , garbage collector is there , so bascially gcnew is (garbage collected new)
			* it used so that the .net runtime know that handle the deletion of this of object.
			gcnew String(...): This creates a new .NET string object with the contents of the character array returned by .c_str().*/
			arr[i] = gcnew String(vec[i].c_str());

		}
		return arr;
	}

	int Entity::ListBucketS3()
	{
		int val = M_Instance->ListBukcet();
		Console::WriteLine("List Bucket return Value :" + val);
		return val;
	}

	void Entity::CloseConnection()
	{
		M_Instance->~AwsConnector();
	}

}