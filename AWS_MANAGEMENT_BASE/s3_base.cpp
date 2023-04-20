#include "s3_base.h"

#include <aws/s3/S3Client.h>
#include <aws/s3/model/ListObjectsV2Request.h>
#include <iostream>
#include <aws/core/auth/AWSCredentials.h>
#include <aws/s3/S3EndpointProvider.h>
#include <vector>

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
}