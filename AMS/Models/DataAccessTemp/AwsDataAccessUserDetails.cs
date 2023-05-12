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

        public UserDetails DataObject { get; set; }

        public string[] AWSParams { get; set; }
        
        public AwsDataAccessUserDetails( UserDetails obj, params string[] awsparams)
        {
            DataObject = obj;

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

            /*----New Code---*/
            Dictionary<string, string> TempStudentData = new Dictionary<string, string>();

            DYNAMODB_BRIDGE dYNAMODB_BRIDGE = new DYNAMODB_BRIDGE();

            int Connection_Result = dYNAMODB_BRIDGE.CreateDynamoDBConnection();

            if (DataObject is StudentUserDetails)
            {
                
                if ( Connection_Result == 1)
                {
                    // TODO: the parameters have been made available in AWSParams array in the following format: Username, password, Batch, Course, Branch, Section.
                    //Use them as required.

                    TempStudentData = dYNAMODB_BRIDGE.GetItems("users_student", "id", AWSParams[0]);
                }

                if (TempStudentData["ExecCode"].Equals("1"))
                {

                    ((StudentUserDetails)DataObject).UserName = TempStudentData["id"];
                    ((StudentUserDetails)DataObject).Semester = TempStudentData["semester"];
                    ((StudentUserDetails)DataObject).Batch = TempStudentData["batch"];
                    ((StudentUserDetails)DataObject).Name = TempStudentData["name"];
                    ((StudentUserDetails)DataObject).Email = TempStudentData["email"];
                    ((StudentUserDetails)DataObject).MobileNumber = TempStudentData["mobilenumber"];
                    ((StudentUserDetails)DataObject).Nationality = TempStudentData["nationality"];
                    ((StudentUserDetails)DataObject).HomeAddress = TempStudentData["homeaddress"];
                    ((StudentUserDetails)DataObject).Hostel = TempStudentData["hostel"];
                    ((StudentUserDetails)DataObject).Branch = TempStudentData["branch"];
                    ((StudentUserDetails)DataObject).BloodType = TempStudentData["bloodtype"];
                    ((StudentUserDetails)DataObject).Course = TempStudentData["course"];
                    ((StudentUserDetails)DataObject).Password = TempStudentData["password"];

                }
                else
                {
                    throw new AMSExceptions.AMSError_Exceptions(TempStudentData["ErrorMessage"]);
                }

                
            }
            else 
            {
                // TODO: Define the functionality Getting Teacher's Data.
                //Get Some Other Type of Data.

                ((OfficialUserDetails)DataObject).UserName = "";
                
                ((OfficialUserDetails)DataObject).Name = "";
                ((OfficialUserDetails)DataObject).Email = "";
                ((OfficialUserDetails)DataObject).MobileNumber = "";
                ((OfficialUserDetails)DataObject).Nationality = "";
                ((OfficialUserDetails)DataObject).HomeAddress = "";
                ((OfficialUserDetails)DataObject).Hostel = "";
                
                ((OfficialUserDetails)DataObject).BloodType = "";
                
                ((OfficialUserDetails)DataObject).Password = "";


            }

            dYNAMODB_BRIDGE.CloseConnection();
            /*----New Code---*/


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
