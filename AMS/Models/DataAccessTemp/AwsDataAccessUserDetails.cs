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

        /*
         * Jashan: For your API, lets assume you need some type of functions that you are providing,
         * I have created a template for that use that function for testing.
         */

        public void TestFunc(params string[] Args) //Use this for testing.
        {
            throw new NotImplementedException();
        }

        public BasicUserDetails DataObject { get; set; }

        public string[] AWSParams { get; set; }
        
        public AwsDataAccessUserDetails( Object obj, params string[] awsparams)
        {
            DataObject = (BasicUserDetails)obj;

            AWSParams = awsparams;

        }

        public void GetData(string? AwsParam = null)
        {

            // TODO: Jashan Code your Logic here, or just provide me a template of which functions to call, and How to call them.
            /*
             * Provide me the template in this comment so that i can use it to create the logic.
             * 
             * your API logic goes here:
             * 
             * 
             */


            Dictionary<string, string> TempStudentData = new Dictionary<string, string>();

            Console.WriteLine(AWSParams[0]);

            DYNAMODB_BRIDGE dYNAMODB_BRIDGE = new DYNAMODB_BRIDGE();

            if(dYNAMODB_BRIDGE.CreateDynamoDBConnection()==1)
            {
                TempStudentData = dYNAMODB_BRIDGE.GetItems("users_student", "id", AWSParams[0]);
            }

            foreach (string keys in TempStudentData.Keys)
            {
                Console.WriteLine(keys + ":" + TempStudentData[keys]);
            }

            if (TempStudentData["ExecCode"].Equals("1"))
            {
                DataObject.UserName = TempStudentData["id"];
                DataObject.Semester = TempStudentData["semester"];
                DataObject.Batch = TempStudentData["batch"];
                DataObject.Name = TempStudentData["name"];
                DataObject.Email = TempStudentData["email"];
                DataObject.MobileNumber = TempStudentData["mobilenumber"];
                DataObject.Nationality = TempStudentData["nationality"];
                DataObject.HomeAddress = TempStudentData["homeaddress"];
                DataObject.Hostel = TempStudentData["hostel"];
                DataObject.Branch = TempStudentData["branch"];
                DataObject.BloodType = TempStudentData["bloodtype"];
                DataObject.Course = TempStudentData["course"];
                DataObject.Password = TempStudentData["password"];
            }
            else
            {
                throw new AMSExceptions.AMSError_Exceptions(TempStudentData["ErrorMessage"]);
            }

            // TODO: If Not possible, then throw exception
            

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }

        
    }
}
