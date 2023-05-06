#include "s3_base.h"

#include <aws/s3/S3Client.h>
#include <aws/s3/model/ListObjectsV2Request.h>
#include <aws/s3/model/GetObjectRequest.h>
#include <iostream>
#include <aws/core/auth/AWSCredentials.h>
#include <aws/s3/S3EndpointProvider.h>
#include <vector>

#include <aws/s3/model/PutObjectRequest.h>
#include <chrono>
#include <condition_variable>
#include <fstream>
#include <mutex>
#include <sys/stat.h>

namespace base
{

	std::shared_ptr<Aws::S3::S3Client> s3clientRef;

	int s3_base::CreateS3Connection()
	{
		Aws::Client::ClientConfiguration config;
		config.region = Aws::Region::AP_SOUTH_1;
		try
		{
			auto s3_client = Aws::MakeShared<Aws::S3::S3Client>("s3client", config);
			s3clientRef = s3_client;
		}
		catch (...)
		{
			return 0;
		}
		return 1;
	}

	s3_base::~s3_base()
	{
		s3clientRef = nullptr;
	}

	std::vector<std::string> s3_base::ListObjects()
	{

		Aws::S3::Model::ListObjectsV2Request request;
		request.WithBucket("ams-test-bucket1");
		request.WithDelimiter(".");
		request.WithPrefix("");

		std::vector<std::string> result;

		auto outcome = s3clientRef->ListObjectsV2(request);

		if (outcome.IsSuccess())
		{
			Aws::Vector<Aws::S3::Model::Object> objects =
				outcome.GetResult().GetContents();

			for (Aws::S3::Model::Object& object : objects) {

				result.push_back(object.GetKey());

			}
		}
		else
		{
			std::cout << "Failed with error: " << outcome.GetError() << std::endl;
			return result;
		}
		int ii;
		std::cin >> ii;

		return result;
	}

	std::vector<std::string> s3_base::ListBucket()
	{
		std::vector<std::string> result;

		auto outcome = s3clientRef->ListBuckets();

		if (outcome.IsSuccess())
		{
			for (auto&& b : outcome.GetResult().GetBuckets())
			{
				result.push_back(b.GetName());
			}
			return result;
		}

		return result;
	}

	//FUNCTIONS TO UPLOAD OBJECT TO S3 - START

	// snippet-start:[s3.cpp.put_object_async.mutex_vars]
	// A mutex is a synchronization primitive that can be used to protect shared
	// data from being simultaneously accessed by multiple threads.
	std::mutex upload_mutex;

	// A condition_variable is a synchronization primitive that can be used to
	// block a thread, or to block multiple threads at the same time.
	// The thread is blocked until another thread both modifies a shared
	// variable (the condition) and notifies the condition_variable.
	std::condition_variable upload_variable;

	void PutObjectAsyncFinished(const Aws::S3::S3Client* s3Client,
		const Aws::S3::Model::PutObjectRequest& request,
		const Aws::S3::Model::PutObjectOutcome& outcome,
		const std::shared_ptr<const Aws::Client::AsyncCallerContext>& context) {
		if (outcome.IsSuccess()) {
			std::cout << "Success: PutObjectAsyncFinished: Finished uploading '"
				<< context->GetUUID() << "'." << std::endl;
		}
		else {
			std::cerr << "Error: PutObjectAsyncFinished: " <<
				outcome.GetError().GetMessage() << std::endl;
		}

		// Unblock the thread that is waiting for this function to complete.
		upload_variable.notify_one();
	}


	bool PutObjectAsync(
		const Aws::String& bucketName,
		const Aws::String& fileName,
		const Aws::String& ObjectKey
	)
	{

		// Create and configure the asynchronous put object request.
		Aws::S3::Model::PutObjectRequest request;
		request.SetBucket(bucketName);
		request.SetKey(ObjectKey);

		const std::shared_ptr<Aws::IOStream> input_data =
			Aws::MakeShared<Aws::FStream>("SampleAllocationTag",
				fileName.c_str(),
				std::ios_base::in | std::ios_base::binary);

		if (!*input_data) {
			std::cerr << "Error: unable to open file " << fileName << std::endl;
			return false;
		}

		request.SetBody(input_data);

		// Create and configure the context for the asynchronous put object request.
		std::shared_ptr<Aws::Client::AsyncCallerContext> context =
			Aws::MakeShared<Aws::Client::AsyncCallerContext>("PutObjectAllocationTag");
		context->SetUUID(fileName);

		// Make the asynchronous put object call. Queue the request into a 
		// thread executor and call the PutObjectAsyncFinished function when the 
		// operation has finished. 
		s3clientRef->PutObjectAsync(request, PutObjectAsyncFinished, context);

		return true;
	}


	bool s3_base::UploadObject(const std::string bucketName,
		const std::string fileName,
		const std::string ObjectKey)
	{
		std::unique_lock<std::mutex> lock(upload_mutex);

		bool upload_bool = PutObjectAsync(bucketName, fileName, ObjectKey);

		upload_variable.wait(lock);

		return upload_bool;
	}

	//FUNCTIONS TO UPLOAD OBJECT TO S3 - END

	//FUNCTION TO DOWNLOAD / GET OBJECT FROM S3
	std::vector<std::string> s3_base::GetObject(const std::string objectKey, 
		const std::string fromBucket,
		const std::string SavePath
	)
	{

		std::vector<std::string> result;
		result.push_back("1");
		result.push_back("1");

		Aws::S3::Model::GetObjectRequest request;
		request.SetBucket(fromBucket);
		request.SetKey(objectKey);

		Aws::S3::Model::GetObjectOutcome outcome = s3clientRef->GetObject(request);

		if (!outcome.IsSuccess()) {
			const Aws::S3::S3Error& err = outcome.GetError();
			result[0] = "0";
			result[1] = err.GetExceptionName() + ":" + err.GetMessage();
		}
		else {
			result[0] = "1";

			const auto& response = outcome.GetResultWithOwnership();
			
			std::ofstream file(SavePath, std::ios::binary);


			if (file.is_open())
			{
				const std::streamsize buffer_size = 1024;
				std::streamsize bytes_read = 0;
				char buffer[buffer_size];

				while (bytes_read = response.GetBody().read(buffer, buffer_size).gcount()) {
					file.write(buffer, bytes_read);
				}

				result[1] = "";

			}
			else
			{
				result[0] = "0";
				result[1] = "error opening file";
			}

			file.close();

		}

		return result;
	}
}