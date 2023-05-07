#pragma once

#include "AWS_MANAGEMENT_BRIDGE.h"

#include "../AWS_MANAGEMENT_BASE/core.h"

using namespace System;

namespace AWS_MANAGEMENT_BRIDGE
{
	public ref class S3_BRIDGE : public ManagedObject<base::s3_base>
	{
	public:
		S3_BRIDGE();
		int CreateS3Connection();
		array<String^>^ ListObjectsS3();
		array<String^>^ ListBucketS3();
		bool PutObjectS3(String^ bucketName, String^ filePath, String^ ObjectKey);
		array<String^>^ GetObjectS3(String^ objectKey, String^ fromBucket, String^ SavePath);
		void CloseConnection();
	};
}