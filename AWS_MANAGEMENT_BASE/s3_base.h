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

		int CreateS3Connection();

	};
}