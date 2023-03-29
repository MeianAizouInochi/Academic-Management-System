using AMS.Models.AwsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Dashboard
    {
        /*
         * The Required Fields/Properties need to be collected from Clients(i.e. Sakhshi Mam)
         */
        public string Name;
        public string ID;
        public DateTime DOB;

        //Constructor
        public Dashboard()
        {
            AwsManager.Aws_Manager awsManager = new AwsManager.Aws_Manager(); //handle the data read functionality here.

            //Update the Members here.
        }


    }
}
