
#include <aws/core/Aws.h>
#include <aws/core/utils/logging/LogLevel.h>
#include <aws/s3/S3Client.h>
#include <aws/s3/model/ListObjectsV2Request.h>
#include <iostream>
#include <aws/core/auth/AWSCredentials.h>
#include <aws/s3/S3EndpointProvider.h>
#include "AwsConnector.h"
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
			auto s3_client = Aws::MakeShared<Aws::S3::S3Client>("s3client", credentials, endpointProvider, config);
			s3clientRef = s3_client;
		}
		catch (...)
		{
			return 0;
		}
		return 1;
	}

	AwsConnector::~AwsConnector()
	{
		s3clientRef = nullptr;
		ShutdownAPI(options);
	}

	std::vector<std::string> AwsConnector::ListObjects()
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

	std::vector<std::string> AwsConnector::ListBukcet()
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
}