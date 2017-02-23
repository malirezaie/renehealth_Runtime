using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeKit
{
    class CalendarController
    {
        public static HttpClient Client = new HttpClient();
        public static CalendarController Instance = new CalendarController();

        public static void Initialize()
        {

            //add timekit app headers
            //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            //  "Basic",
            //   Convert.ToBase64String(
            //         System.Text.Encoding.UTF8.GetBytes(
            //             string.Format("{0}:{1}", azureUser.email, azureUser.api_token))));

            //Client.DefaultRequestHeaders.Add("Timekit-App", TIMEKIT_APP_ID);
            //Client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            
        }


    }
}
