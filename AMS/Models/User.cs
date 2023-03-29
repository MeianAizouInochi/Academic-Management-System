using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class User<T>
    {
        public T UserType;

        //constructor for setting the value of the User Type
        public User( T userType)
        {
            UserType = userType;
        }
    }
}
