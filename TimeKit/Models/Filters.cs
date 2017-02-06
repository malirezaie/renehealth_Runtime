using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKit.Models
{
    public static class Filters
    {
        static List<string> days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        
        //public static JObject andFilter(params JObject[] list)
        //{
        //    return new JObject(new JProperty("and"), new JArray(list));
        //}

        //public static JObject orFilter(params JObject[] list)
        //{
        //    return new JObject(new JProperty("or"), new JArray(list));
        //}

        public static JObject daytimeFilter(string timeZone = null)
        {
            return new JObject(new JProperty("daytime", new JObject(new JProperty("timezone", timeZone))));
        }
        public static JObject businessHoursFilter(string timeZone = null)
        {
            return new JObject(new JProperty("business_hours", new JObject(new JProperty("timezone", timeZone))));
        }

        public static JObject onlyWeekendFilter()
        {
            return new JObject(new JProperty("only_weekend"));
        }
        public static JObject excludeWeekendFilter()
        {
            return new JObject(new JProperty("exclude_weekend"));
        }

        public static JObject specDayFilter(string _day)
        {
            var day = days.Contains(_day) ? _day : "Monday";

            return new JObject(new JProperty("specific_day", new JObject(new JProperty("day", day))));
        }

        public static JObject specTimeFilter(int _start, int _end)
        {
            int start, end;
            if (_start > -1 && _end < 24 && _start < _end)
            {
                start = _start;
                end = _end;
                return new JObject(new JProperty("specific_time", new JObject(new JProperty("start", start), new JProperty("end",end))));
            }
            else
            {
                return null;
            }

        }
    }
}
