using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.DataAccessTemp
{
    public class AwsDataAccessUserDetails : IDataAccess
    {
        public Object DataObject { get; set; }

        public string[] AWSParams { get; set; }
        
        public AwsDataAccessUserDetails( Object obj, params string[] awsparams)
        {
            DataObject = obj;

            AWSParams = awsparams;

        }

        public void GetData(string? AwsParam = null)
        {
            // TODO: Do functionality

            /*
             * List<string> list = SOmfunc(AWSParams);
             * 
             * DataObject.Username = list[0];
             */

            // TODO: If Not possible, then throw exception
            throw new AMSExceptions.AMSError_Exceptions("User Credentials are wrong");

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }
    }
}
