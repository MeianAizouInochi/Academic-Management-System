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
            string[] exc2;
            int Connection_Result = dYNAMODB_BRIDGE.CreateDynamoDBConnection();

            if (DataObject is StudentUserDetails)
            {
                       
                if ( Connection_Result == 1)
                {
                    // TODO: the parameters have been made available in AWSParams array in the following format: Username, password, Batch, Course, Branch, Section.
                    //Use them as required.

                    TempStudentData = dYNAMODB_BRIDGE.GetItems("2020-2024_BTECH_CSE_AB", "id", AWSParams[0]);

                    exc2 = dYNAMODB_BRIDGE.ScanTable("users_student", "id");

                    if (exc2[0].Equals("1"))
                    {
                        foreach (string exc3 in exc2) { Console.WriteLine(exc3); }         
                    }
                    else
                    {
                        Console.WriteLine("ERROR HAPPENED !");
                    }

                }

                if (TempStudentData["ExecCode"].Equals("1"))
                {

                    ((StudentUserDetails)DataObject).UserName = TempStudentData["id"];
                    ((StudentUserDetails)DataObject).Semester = TempStudentData["0"];
                    //((StudentUserDetails)DataObject).Batch = TempStudentData["batch"];
                    ((StudentUserDetails)DataObject).Name = TempStudentData["1"];
                    ((StudentUserDetails)DataObject).Email = TempStudentData["2"];
                    ((StudentUserDetails)DataObject).MobileNumber = TempStudentData["3"];
                    ((StudentUserDetails)DataObject).Nationality = TempStudentData["4"];
                    ((StudentUserDetails)DataObject).HomeAddress = TempStudentData["5"];
                    ((StudentUserDetails)DataObject).Hostel = TempStudentData["6"];
                    //((StudentUserDetails)DataObject).Branch = TempStudentData["branch"];
                    ((StudentUserDetails)DataObject).BloodType = TempStudentData["8"];
                    //((StudentUserDetails)DataObject).Course = TempStudentData["course"];
                    //((StudentUserDetails)DataObject).Password = TempStudentData["password"];

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

        public void UploadData(string s) 
        {

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }

        //Teacher_user/ Teachers: SakshiXXXXXX



        // TODO: Function to get the list of Students of a particular Batch/Course/Branch/Section,
        // to get the list of students in a table by their roll no 
        // use function ScanTable();
        //  accepted parameters - table name,
        //      projectExpression (for example if you want the list of students in a table by their roll no i.e id just type id) Example in new line
        //      ScanTable("tablename","id"); - this will give the list of all students inside a table ,WARNING - DO NOT LEAVE THE SECOND ARGUMENT BLANK 
        //  The function returns a array of stings in which the first and the second index is the code if the operation was successful or not 
        //  0th index - 0 or 1 (0 - failed/unsuccessfully) 1(successful)
        //  1st index - error message ( what happened exactly) 
        // note: the function returns error in the case when there is no item inside the table of that attribute 
        // after the second index , you have your data or the list of the students.
        // Function returns Array of string.



        // TODO: Function to push the attendance data inside s3.
        // to push attendance data inside s3 
        // use function PutObjectS3();
        // accepted parameters - bucket name - the bucket in s3 where you want to put the item (ams-test-bucket1)
        //                       file path - the file path of the file you want to upload ( where the file is located inside your directory)
        //                       object key - at which name you want to store the file as inside the s3 bucket example on newline
        //                                                2020-2024_BTECH_CSE_AB/2026973/OWL.png (2020-2024_BTECH_CSE_AB - folder in the bucket)
        //                                                                                         (2026973 - folder inside 2020-2024_BTECH_CSE_AB folder)
        //                                                                                           (OWL.png - file name you want to store as inside the 2026973 folder )
        // function returns a boolean value if the object has been successfully put inside the s3. (0 - failed) (1 - successfull).
        // example - PutObjectS3("ams-test-bucket1", "C:\Users\js400\AppData\Local\Temp\56861a8d-7110-458c-aac5-88831f705481.tmp", "2020-2024_BTECH_CSE_AB/2026973/garbage.txt");

        /*
         * Date/Slot/TeacherID/Subject/Q2:{ID Value}
         */


        // TODO: Function to get the attendance data from S3.
        // to get data from s3
        // use function GetObjectS3(objectkey,  bucketname,  savepath);
        //                       objectkey - from file you want to download from the s3 , ex 2020-2024_BTECH_CSE_AB/2026973/OWL.png
        //                       bucket name - which bucket you want to fetch from. (ams-test-bucket1)
        //                       save path - where you want to store the file in your local directory.
        //                      
        //  The function returns a array of stings in which the first and the second index is the code if the operation was successful or not 
        //  0th index - 0 or 1 (0 - failed/unsuccessfully) 1(successful)
        //  1st index - error message ( what happened exactly) 


        // TODO: Function to push the data into the specific items inside dynamoDb.

    }
}
