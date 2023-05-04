using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMS.AMSExceptions
{
    public class AMSError_Exceptions:Exception
    {
        public AMSError_Exceptions(string ExceptionType):base(ExceptionType) 
        {
            //do what is required when exception occurs.

        }
    }
}
