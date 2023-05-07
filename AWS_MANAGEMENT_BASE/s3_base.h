#pragma once

#include <iostream>
#include <vector>

namespace base
{
	class __declspec(dllexport) s3_base
	{
	public:

		s3_base() {};

		~s3_base();

		std::vector<std::string> ListObjects();

		std::vector<std::string> ListBucket();

		bool UploadObject(const std::string bucketName,
			const std::string fileName,
			const std::string ObjectKey);

		std::vector<std::string> GetObject(const std::string objectKey, const std::string fromBucket, const std::string SavePath);

		int CreateS3Connection();

	};
}