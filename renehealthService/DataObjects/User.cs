using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKit.Models;

namespace renehealthService.DataObjects
{
    public class TimeKitUser : EntityData
    {
        public string email { get; set; }
        public string timezone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string socialID { get; set; }

        public TimeKitUser()
        {

        }
        public TimeKitUser(TimekitUser _tkuser)
        {
            email = _tkuser.email;
            timezone = _tkuser.timezone;
            first_name = _tkuser.first_name;
            last_name = _tkuser.last_name;
            socialID = null;
            Id = _tkuser.email;
        }

    }
    public class User
    {

    }
}