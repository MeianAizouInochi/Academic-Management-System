#pragma once

#include <iostream>
#include <vector>

namespace core
{
	class __declspec(dllexport) AwsConnector
	{
	public:

		AwsConnector();

		~AwsConnector();

		std::vector<std::string> ListObjects();

		int ListBukcet();

		int CreateS3Connection();

	};
}
