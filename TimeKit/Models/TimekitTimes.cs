using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKit.Models
{
    public class TimekitTimes
    {
        public List<Times> data { get; set; }
    }

    public class Times
    {
        public string start { get; set; }
        public string end { get; set; }
    }

}
