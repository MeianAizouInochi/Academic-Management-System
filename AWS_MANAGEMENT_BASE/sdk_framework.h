#pragma once
#include <aws/core/Aws.h>
#include <aws/core/utils/logging/LogLevel.h>

class framework_base{
public:
	framework_base()
	{
		
		options.loggingOptions.logLevel = Aws::Utils::Logging::LogLevel::Info; // This creates log file , highly helpful!
		InitAPI(options);
	}
	~framework_base()
	{
		ShutdownAPI(options);
	}
private:
	Aws::SDKOptions options;
};