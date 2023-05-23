using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AMS.AMSExceptions;
using AWS_MANAGEMENT_BRIDGE;

namespace AMS.Models.DataAccessTemp
{
    /// <summary>
    /// This Class Inherits from IDataAccess interface, and Serves as the Class Mediator for Connection to the C++/Cli API.
    /// </summary>
    public class AwsDataAccessUserDetails : IDataAccess
    {
        public UserDetails? DataObject { get; set; }

        public string[]? AWSParams { get; set; }

        //For Simplification of Accessing the Parameters from the string array.
        private enum StudentLoginInfoType
        {
            Batch = 2,

            Course,

            Branch,

            Section

        }

        //For Accessing Stuff with Just Names of those Details.
        private enum StudentDataKeys
        {
            Name,
            Semester,
            Email,
            MobileNumber,
            Nationality,
            HomeAddress,
            HostelFacilityAvailed,
            Password,
            BloodType,
            Age,
            AttendanceRecord
        }

        //For Simplification of Accessing the Parameters from the string array.
        private enum OfficialLoginInfoType
        {
            Details1 = 2,

            Details2
        }

        public void TestFunc(params string[] Args) //Use this for testing.
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructor of AwsDataAccessUserDetails Class.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="awsparams"></param>
        public AwsDataAccessUserDetails(UserDetails obj, params string[] awsparams)
        {
            DataObject = obj;

            AWSParams = awsparams;

        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AwsDataAccessUserDetails()
        {
            //Default Constructor
        }

        /// <summary>
        /// This Function is used to get the User Details of the user who Logs in from the Client.
        /// These Information/Details will be displayed in the Dashboard or can be used for other purposes.
        /// </summary>
        /// <param name="AwsParam"></param>
        /// <exception cref="AMSExceptions.AMSError_Exceptions"></exception>
        public void GetLoggedInUserDetails(string? AwsParam = null)
        {
            Dictionary<string, string> StudentData = new Dictionary<string, string>();

            DYNAMODB_BRIDGE DynamoDBContext = new DYNAMODB_BRIDGE();

            int Connection_Result_Code = DynamoDBContext.CreateDynamoDBConnection();

            if (DataObject is StudentUserDetails)
            {
                //get student Data

                if (Connection_Result_Code == 1) //Success
                {
                    //Getting Data.

                    //Console.WriteLine($"{AWSParams?[(int)StudentLoginInfoType.Batch]}_{(int)StudentLoginInfoType.Course}_{(int)StudentLoginInfoType.Branch}_{(int)StudentLoginInfoType.Section}");

                    StudentData = DynamoDBContext.GetItems(
                        $"{AWSParams?[(int)StudentLoginInfoType.Batch]}_{AWSParams?[(int)StudentLoginInfoType.Course]}_{AWSParams?[(int)StudentLoginInfoType.Branch]}_{AWSParams?[(int)StudentLoginInfoType.Section]}",
                        "id",
                        AWSParams?[0]
                        );

                    if (StudentData["ExecCode"].Equals("1")) //Checking if Executed Correctly
                    {

                        //Setting the data into the Model.

                        ((StudentUserDetails)DataObject).Name = StudentData[((int)StudentDataKeys.Name).ToString()];

                        ((StudentUserDetails)DataObject).Semester = StudentData[((int)StudentDataKeys.Semester).ToString()];

                        ((StudentUserDetails)DataObject).Email = StudentData[((int)StudentDataKeys.Email).ToString()];

                        ((StudentUserDetails)DataObject).MobileNumber = StudentData[((int)StudentDataKeys.MobileNumber).ToString()];

                        ((StudentUserDetails)DataObject).Nationality = StudentData[((int)StudentDataKeys.Nationality).ToString()];

                        ((StudentUserDetails)DataObject).HomeAddress = StudentData[((int)StudentDataKeys.HomeAddress).ToString()];

                        ((StudentUserDetails)DataObject).HostelFacilityAvailed = StudentData[((int)StudentDataKeys.HostelFacilityAvailed).ToString()];

                        ((StudentUserDetails)DataObject).Password = StudentData[((int)StudentDataKeys.Password).ToString()];

                        ((StudentUserDetails)DataObject).BloodType = StudentData[((int)StudentDataKeys.BloodType).ToString()];

                        ((StudentUserDetails)DataObject).Age = StudentData[((int)StudentDataKeys.Age).ToString()];

                        ((StudentUserDetails)DataObject).AttendanceRecord = StudentData[((int)StudentDataKeys.AttendanceRecord).ToString()];

                        ((StudentUserDetails)DataObject).Batch = AWSParams?[(int)StudentLoginInfoType.Batch];

                        ((StudentUserDetails)DataObject).Branch = AWSParams?[(int)StudentLoginInfoType.Branch];

                        ((StudentUserDetails)DataObject).Course = AWSParams?[(int)StudentLoginInfoType.Course];

                        ((StudentUserDetails)DataObject).Section = AWSParams?[(int)StudentLoginInfoType.Section];

                    }
                    else
                    {
                        throw new AMSExceptions.AMSError_Exceptions(StudentData["ErrorMessage"]);
                    }
                }
                else 
                {
                    throw new AMSExceptions.AMSError_Exceptions("Connectivity Error!");
                }
            }
            else if (DataObject is OfficialUserDetails)
            {
                //get official Data

                //TempData
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

            DynamoDBContext.CloseConnection();

            //Getting Profile Picture from S3.

            S3_BRIDGE S3DBContext = new S3_BRIDGE();

            string[] exc;

            if (S3DBContext.CreateS3Connection() == 1)
            {
                //will be later replaced by uploaded profile pic.
                exc = S3DBContext.GetObjectS3("2020-2024_BTECH_CSE_AB/2026973/OWL.png", "ams-test-bucket1", "./owl.png");

                if (exc[0].Equals("0"))
                {
                    //MessageBox.Show(exc[1]);

                    throw new AMSExceptions.AMSError_Exceptions(exc[1]);
                }
                else
                {
                    string rpath = @".\owl.png";

                    DataObject.path = Path.GetFullPath(rpath);
                }
            }

            S3DBContext.CloseConnection();
        }

        /// <summary>
        /// It puts the list of Student Id's into the attribute StudentIdList of the object of StudentList Class.
        /// In Case of Failure it provides null in the attribute.
        /// </summary>
        /// <param name="studentList"></param>
        /// <param name="TableInfo"></param>
        public void GetStudentIDList(StudentList studentList, params string[] TableInfo)
        {
            DYNAMODB_BRIDGE DynamoDbContext = new DYNAMODB_BRIDGE();

            string[] Result;

            if (DynamoDbContext.CreateDynamoDBConnection() == 1)
            {
                
                string TableName = $"{TableInfo[(int)StudentLoginInfoType.Batch - 2]}_{TableInfo[(int)StudentLoginInfoType.Course - 2]}_{TableInfo[(int)StudentLoginInfoType.Branch - 2]}_{TableInfo[(int)StudentLoginInfoType.Section - 2]}";

                Result = DynamoDbContext.ScanTable(
                   TableName, 
                    "id");
                

                if (Result[0].Equals("0"))
                {
                    throw new AMSError_Exceptions(Result[1]);
                }
                else 
                {
                    studentList.StudentIdList = Result;

                }
            }
            else
            {
                throw new AMSError_Exceptions("Connectivity Error!");

            }

        }

        /// <summary>
        /// This function updates the attendance of a specific subject of a student.
        /// It changes 2 values.
        /// 
        /// AttendedClassAddDays represents that the student attended the class that day,
        /// hence the total number of days the student have attended the class is updated by 1.
        /// 
        /// ClassTakenByTeacherAddDays represents the addition to the total number of days the class was taken by the teacher.
        /// 
        /// The Ratio of the above 2 values provides the % of Attendance in the subject of the student.
        /// </summary>
        /// <param name="studentIdList"></param>
        /// <param name="TableInfo"></param>
        /// <param name="AttendanceColumn"></param>
        /// <param name="SubjectCode"></param>
        /// <param name="AttendedClassAddDays"></param>
        /// <param name="ClassTakenByTeacherAddDays"></param>
        /// <exception cref="AMSExceptions.AMSError_Exceptions"></exception>
        public void UpdateStudentAttendance(
            string[] studentIdList,
            string[] TableInfo,
            string AttendanceColumn,
            string SubjectCode,
            string AttendedClassAddDays,
            string ClassTakenByTeacherAddDays)
        {
            DYNAMODB_BRIDGE DynamoDbContext = new DYNAMODB_BRIDGE();

            if (DynamoDbContext.CreateDynamoDBConnection() == 1)
            {
                string[] Result;

                Result = DynamoDbContext.UpdateAttendance(
                    $"{TableInfo[0]}_{TableInfo[1]}_{TableInfo[2]}_{TableInfo[3]}",
                    "id",
                    studentIdList,
                    AttendanceColumn,
                    SubjectCode,
                    AttendedClassAddDays,
                    ClassTakenByTeacherAddDays
                    );

                if (Result[0].Equals("0"))
                {
                    throw new AMSError_Exceptions(Result[1]);
                }

            }
            
        }

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

        public void UploadData(string s)
        {

        }

        public void UpdateData(string? AwsParam = null)
        {
            //do what you need to do
        }
    }
}
