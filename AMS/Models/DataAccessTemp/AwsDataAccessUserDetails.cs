using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS_MANAGEMENT_BRIDGE;

namespace AMS.Models.DataAccessTemp
{
    public class AwsDataAccessUserDetails : IDataAccess
    {
        public BasicUserDetails DataObject { get; set; }

        public string[] AWSParams { get; set; }
        
        public AwsDataAccessUserDetails( Object obj, params string[] awsparams)
        {
            DataObject = (BasicUserDetails)obj;

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

            Dictionary<string, string> dic = new Dictionary<string, string>();

            DYNAMODB_BRIDGE dYNAMODB_BRIDGE = new DYNAMODB_BRIDGE();
            if(dYNAMODB_BRIDGE.CreateDynamoDBConnection()==1)
            {
                dic = dYNAMODB_BRIDGE.GetItems("users_student", "id", AWSParams[0]);
            }

            if (dic["ExecCode"].Equals("1"))
            {
                DataObject.UserName = dic["name"];
            }
            else
            {
                throw new AMSExceptions.AMSError_Exceptions("User Credentials are wrong");
            }

            // TODO: If Not possible, then throw exception
            

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }
    }
}
