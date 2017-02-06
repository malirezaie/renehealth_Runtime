﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKit.Models
{
    public class TimekitUser
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public string timezone { get; set; }
        //public string created_at { get; set; }
        //public string updated_at { get; set; }
        public string api_token { get; set; }

    }
}
