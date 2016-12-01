using System;
using System.Collections.Generic;
using System.Text;

namespace Mark.VersionRouter
{
    public class Package
    {
        public string Version { get; set; }

        public List<Client> Client { get; set; }
    }
}
