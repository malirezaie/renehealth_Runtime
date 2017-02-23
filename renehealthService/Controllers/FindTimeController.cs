using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using renehealthService.Models;
using renehealthService.DataObjects;
using System.Threading.Tasks;

using Timekit;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace renehealthService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    [RoutePrefix("api/v0.1")]
    public class FindTimeController : ApiController
    {
        renehealthContext context;


        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            context = new renehealthContext();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }
       
        // GET api/time/v0.1
        [HttpGet, Route("findtime")]
        public async Task<AvailableTimes> FindAllTimes(string email1, string email2, string timezone1, string timezone2, int numDays = 7, string interval = "30 minutes")
        {
            var result = await TimeKitController.Instance.GetAllTimesAsync(email1, email2, timezone1, timezone2,interval, numDays);

            var response = new AvailableTimes(result.RootObject);

            response.timeInterval = interval;

            return response;

        }

        // POST api/values
        public string Post()
        {
            return "Hello World!";
        }
    }
}
