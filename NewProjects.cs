using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Report_Generator
{
    class NewProjects
    {
        //public string Name;

        public NewProjects() { }

        public string ProjectsName { get; set; }

        public DateTime ProjectsDateStarted { get; set; }
        public DateTime ProjectsDeadline { get; set; }
        public DateTime ProjectsDateFinished { get; set; }
    }
}
