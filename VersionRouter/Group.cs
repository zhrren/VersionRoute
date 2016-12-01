using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mark.VersionRouter
{
    public class Group
    {
        public Group() { }
        public Group(string name, string[] user)
        {
            Name = name;
            Users = user;
        }

        public string Name { get; set; }
        
        public string[] Users { get; set; }
    }
}
