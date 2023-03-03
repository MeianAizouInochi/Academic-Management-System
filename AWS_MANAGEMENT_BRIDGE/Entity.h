#pragma once

#include "AWS_MANAGEMENT_BRIDGE.h"

#include "../AWS_MANAGEMENT_BASE/core.h"

using namespace System;

namespace AWS_MANAGEMENT_BRIDGE
{
	public ref class Entity : public ManagedObject<core::AwsConnector>
	{
	public:
		Entity();
		int CreateS3Connection();
		array<String^>^ ListObjectsS3();
		array<String^>^ ListBucketS3();
		void CloseConnection();
	};
}