using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Teacher
    {
        public Dashboard dashboard;

        public Announcement_Reader announcement_Reader;

        public TimeTable_CRUD timeTable_CRUD;

        public SODCM_CRUD sodcm_CRUD;

        public SSV_RR ssv_RR;

        public Form_CRUD form_CRUD;

        //constructor to initialize the Members of the Class.
        public Teacher()
        {
            dashboard= new Dashboard();
            
            announcement_Reader = new Announcement_Reader();
            
            timeTable_CRUD= new TimeTable_CRUD();
            
            sodcm_CRUD= new SODCM_CRUD();
            
            ssv_RR= new SSV_RR();
            
            form_CRUD = new Form_CRUD();
        }
    }
}
