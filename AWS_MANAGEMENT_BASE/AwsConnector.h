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

		std::vector<std::string> ListBukcet();

		int CreateS3Connection();

	};
}
