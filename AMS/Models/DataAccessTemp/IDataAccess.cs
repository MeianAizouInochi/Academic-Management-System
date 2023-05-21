using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.DataAccessTemp
{
    public interface IDataAccess
    {
        string[]? AWSParams { get; set; }

        void GetLoggedInUserDetails(string? Params = null);

        void UpdateData(string? Params = null);

        void TestFunc(params string[] Args);

        void GetStudentIDList(StudentList studentList,params string[] TableInfo);

    }
}
