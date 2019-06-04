using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Report_Generator
{
    class OldProjects
    {
        /*name;
        date;
        projlead;
        desc;
        teamlead;
        statuschange;
        code;
        deadline;*/

        /*public string Name;
        public int Date;
        public string ProjLead;
        public string Desc;
        public string TeamLead;
        public string StatusChanged;
        public string Code;
        public int Deadline;*/

        public OldProjects() { }

        public string ProjectsName { get; set; }
        public int ProjectsDate { get; set; }
        public string ProjectsProjLead { get; set; }
        public string ProjectsDesc { get; set; }
        public string ProjectsTeamLead { get; set; }
        public string ProjectsStatusChanged { get; set; }
        public string ProjectsCode { get; set; }
        public int ProjectsDeadline { get; set; }
    }
}
