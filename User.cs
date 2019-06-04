using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Report_Generator
{
    class User
    {
        /*public int Id;
        public string Name;
        public string Department;
        public string Picture;
        public string Privilege;*/

        public User() { }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserDepartment { get; set; }
        public string UserPicture { get; set; }
        public string UserPrivilege { get; set; }
    }
}
