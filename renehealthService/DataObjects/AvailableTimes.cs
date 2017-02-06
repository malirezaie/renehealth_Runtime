using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKit.Models;

namespace renehealthService.DataObjects
{
    public class AvailableTimes
    {
        public List<AvailableDay> dayslist = new List<AvailableDay>();
        public string timeInterval { get; set; }

        //constructor that parses the object
        public AvailableTimes(TimekitTimes timekitTimes)
        {
            //get all the days
            string nextday = "";
            List<string> daysList = new List<string>();
            int dayIndex = -1;
            foreach (var days in timekitTimes.data)
            {
                var splitstring = days.start.Split('T');
                if (!nextday.Equals(splitstring[0]))
                {
                    dayIndex++;
                    nextday = days.start.Split('T')[0];
                    dayslist.Add(new AvailableDay { day = nextday });
                    dayslist[dayIndex].timesList.Add(splitstring[1]);
                }
                else
                {
                    dayslist[dayIndex].timesList.Add(splitstring[1]);
                }
            }
        }

    }

    public class AvailableDay
    {
        public string day { get; set; }
        public List<string> timesList = new List<string>();

    }

}