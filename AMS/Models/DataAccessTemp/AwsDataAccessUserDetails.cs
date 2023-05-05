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

            Console.WriteLine(AWSParams[0]);

            DYNAMODB_BRIDGE dYNAMODB_BRIDGE = new DYNAMODB_BRIDGE();
            if(dYNAMODB_BRIDGE.CreateDynamoDBConnection()==1)
            {
                dic = dYNAMODB_BRIDGE.GetItems("users_student", "id", AWSParams[0]);
            }

            foreach (string keys in dic.Keys)
            {
                Console.WriteLine(keys + ":" + dic[keys]);
            }

            if (dic["ExecCode"].Equals("1"))
            {
                DataObject.UserName = dic["id"];
                DataObject.Semester = dic["semester"];
                DataObject.Batch = dic["batch"];
                DataObject.Name = dic["name"];
                DataObject.Email = dic["email"];
                DataObject.MobileNumber = dic["mobilenumber"];
                DataObject.Nationality = dic["nationality"];
                DataObject.HomeAddress = dic["homeaddress"];
                DataObject.Hostel = dic["hostel"];
                DataObject.Branch = dic["branch"];
                DataObject.BloodType = dic["bloodtype"];
                DataObject.Course = dic["course"];
                DataObject.Password = dic["password"];
            }
            else
            {
                throw new AMSExceptions.AMSError_Exceptions(dic["ErrorMessage"]);
            }

            // TODO: If Not possible, then throw exception
            

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }
    }
}
