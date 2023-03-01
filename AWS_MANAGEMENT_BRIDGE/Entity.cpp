#include "Entity.h"
#include <vector>

namespace AWS_MANAGEMENT_BRIDGE
{


	/*Entity class constructor , when called makes a new object of AwsConnetor class and pass 
	it to the ManagedObject classtemplate(M_Instance) , so that it can store the pointer of the newly created 
	object of the AwsConnector class
	*/
	Entity::Entity() : ManagedObject(new core::AwsConnector()){}


	/*This function creates creates a s3client connection and pass a int value (o)False or (1)True
	* if the connection making was failed or successful.
	*/
	int Entity::CreateS3Connection()
	{

		int value = M_Instance->CreateS3Connection();

		return value;

	}


	/*This Function return a .net Datatype so that it can be consumed by C# application
	this function list all the objects present in a S3bucket 
	*/
	array<String^>^ Entity::ListObjectsS3()
	{

		//getting data from the ListObjects() function in the AwsConnector class in form of a vector.
		std::vector<std::string> Temp_vector = M_Instance->ListObjects();

		int size = Temp_vector.size();

		//here creating a array of .net strings  
		array<String^>^ Array_Of_Objects = gcnew array<String^>(size);//The gcnew keyword is used to allocate a new managed object on the .NET heap.


		for (int i = 0; i < size; i++)
		{
			/*.c_str(): This returns a pointer to a null-terminated array of characters representing the contents of the std::string.
			* gcnew is same a new but as for managed languages , garbage collector is there , so bascially gcnew is (garbage collected new)
			* it used so that the .net runtime know that handle the deletion of this of object.
			gcnew String(...): This creates a new .NET string object with the contents of the character array returned by .c_str().*/
			Array_Of_Objects[i] = gcnew String(Temp_vector[i].c_str());

		}

		return Array_Of_Objects;

	}

	/*This function list all the buckets*/
	array<String^>^ Entity::ListBucketS3()
	{
		std::vector<std::string> Temp_vector = M_Instance->ListBukcet();
		int size = Temp_vector.size();
		array<String^>^ Array_of_Buckets = gcnew array<String^>(size);

		for (int i = 0; i < size; i++)
		{
			Array_of_Buckets[i] = gcnew String(Temp_vector[i].c_str());
		}

		return Array_of_Buckets;
	}

	/*This function closes the connection to s3client and also shuts down the sdk*/
	void Entity::CloseConnection()
	{
		M_Instance->~AwsConnector();
		M_Instance = nullptr;
	}

}