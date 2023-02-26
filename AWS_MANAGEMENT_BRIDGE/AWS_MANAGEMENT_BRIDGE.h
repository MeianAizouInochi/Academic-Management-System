#pragma once

using namespace System;

namespace AWS_MANAGEMENT_BRIDGE {
	template<class T>

	public ref class ManagedObject
	{

	protected:
		T* M_Instance;

	public:
		ManagedObject(T* instance)
			: M_Instance(instance)
		{}

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
