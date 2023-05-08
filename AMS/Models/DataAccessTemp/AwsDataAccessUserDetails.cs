using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
             * GetObjectS3() function which retrives files from S3
             * parameter accepted 
             * 1. 
             * output
             * 0th index  - 0/1 0-unsucessfull , 1- sucessfull
             * 1th index  - message
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

            dYNAMODB_BRIDGE.CloseConnection();

            S3_BRIDGE s3_BRIDGE = new S3_BRIDGE();
            string[] exc;

            if(s3_BRIDGE.CreateS3Connection()==1)
            {

                exc = s3_BRIDGE.GetObjectS3("2020-2024_BTECH_CSE_AB/2026973/OWL.png", "ams-test-bucket1", "./owl.png");
                if (exc[0].Equals("0"))
                {
                    MessageBox.Show(exc[1]);
                }
                else
                {
                    string rpath = @".\owl.png";
                    DataObject.path = Path.GetFullPath(rpath);
                }
            }
            s3_BRIDGE.CloseConnection();
            // TODO: If Not possible, then throw exception
            

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }

        
    }
}
