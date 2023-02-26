
#include <aws/core/Aws.h>
#include <aws/core/utils/logging/LogLevel.h>
#include <aws/s3/S3Client.h>
#include <aws/s3/model/ListObjectsV2Request.h>
#include <iostream>
#include <aws/core/auth/AWSCredentials.h>
#include <aws/s3/S3EndpointProvider.h>
#include "AwsConnector.h"
#include <fstream>
#include <vector>

namespace core
{

	std::shared_ptr<Aws::S3::S3Client> s3clientRef;

	Aws::SDKOptions options;

	AwsConnector::AwsConnector()
	{

		options.loggingOptions.logLevel = Aws::Utils::Logging::LogLevel::Info; // This creates log file , highly helpful!

		InitAPI(options);

	}

	int AwsConnector::CreateS3Connection()
	{
		std::shared_ptr<Aws::S3::S3EndpointProviderBase> endpointProvider = Aws::MakeShared<Aws::S3::S3EndpointProvider>("asas");
		Aws::Auth::AWSCredentials credentials("AKIAT4U4GUL6XSLEXLPW", "8Wd4HIoQeWfshMsq/nCdNvCYQkT9nejBl8+wRiKc");
		Aws::Client::ClientConfiguration config;
		config.region = Aws::Region::AP_SOUTH_1;
		try
		{
			std::cout << "Came in try block" << std::endl;
			auto s3_client = Aws::MakeShared<Aws::S3::S3Client>("s3client", credentials, endpointProvider, config);
			s3clientRef = s3_client;
		}
		catch (...)
		{
			std::cout << "S3 connection not made" << std::endl;
			return 0;
		}
		return 1;
	}

	AwsConnector::~AwsConnector()
	{
		std::cout << "Destructor is called !" << std::endl;
		ShutdownAPI(options);
	}

	std::vector<std::string> AwsConnector::ListObjects()
	{
		std::fstream fio;
		fio.open("LISTOFOBJECTS.txt", std::ios::in);
		std::cout << "CAme after s3 client" << std::endl;

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

			std::cout << "No of objects in the bucket ?" << outcome.GetResult().GetKeyCount() << std::endl;
			std::cout << "No of objects in the bucket ?" << outcome.GetResult().GetKeyCount() << std::endl;

			for (Aws::S3::Model::Object& object : objects) {

				std::cout << object.GetKey() << std::endl;

				result.push_back(object.GetKey());

				objects.erase(objects.begin(), objects.begin() + 1);

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

	int AwsConnector::ListBukcet()
	{

		auto outcome = s3clientRef->ListBuckets();

		if (outcome.IsSuccess())
		{
			std::cout << "NO OF BUCKETS" << outcome.GetResult().GetBuckets().size() << "\n";
			std::cout << "id of the owner" << outcome.GetResult().GetOwner().GetID() << "\n";
			std::cout << "Name of the owner" << outcome.GetResult().GetOwner().GetDisplayName() << "\n";
			std::cout << "outcome was a success ?" << outcome.IsSuccess() << " \n";
			for (auto&& b : outcome.GetResult().GetBuckets())
			{
				std::cout << b.GetName() << std::endl;
			}
			return 1;
		}
		else
		{
			return 0;
		}
	}
}