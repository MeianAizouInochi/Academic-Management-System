#pragma once

using namespace System;
using namespace System::IO;

namespace AWS_MANAGEMENT_BRIDGE {

	//TEMPLATE 
	template<class T>

	public ref class ManagedObject
	{

	public:
		T* M_Instance;//TEMPLATE POINTER TO THAT OBJECT.

	public:

		ManagedObject(T* instance): M_Instance(instance){}//CONSTRUCTOR

		virtual ~ManagedObject()//DESTRUCTOR
		{
			if (M_Instance != nullptr)
			{
				delete M_Instance;
			}
		}

		!ManagedObject()//THIS THING IS CALLED BY GARBAGE COLLECTOR
		{
			if (M_Instance != nullptr)
			{
				delete M_Instance;
			}
		}

		T* GetInstance()
		{
			return M_Instance;
		}

	};
}
