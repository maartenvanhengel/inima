using Org.XmlPull.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inima.classes
{
    internal class Medicine
    {
        public string name { get; set; }
        public TimeSpan time { get;set; }
        public bool isTaken { get; set; }
        public override string ToString()
        {
            return name + "°" + time + "°" + isTaken;
        }
    }
}
