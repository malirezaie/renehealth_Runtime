using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using TimeKit.Models;

namespace Timekit
{
    public class TimeKitController
    {
        public static HttpClient Client = new HttpClient();
        public static TimeKitController Instance = new TimeKitController();

        static TimekitUser azureUser = azureUser = new TimekitUser
        {
            api_token = "P5JsQ7CXQhOPrzdzolSgJfVcGFL1MNK4",
            timezone = "America/Los_Angeles",
            email = "backend@rene.com",
            first_name = "Azure",
            last_name = "Service",
            id = "1261a09b-cb18-41b7-954a-a6543b98b4c0",

        };

        protected const string TIMEKIT_API_BASE_URL = "https://api.timekit.io/v2/";
        protected static string TIMEKIT_API_FINDTIME_URL;
        protected static string TIMEKIT_API_USERS_URL;
        protected static string TIMEKIT_API_CONVERSATIONS_URL;
        protected static string TIMEKIT_APP_ID;

        public static void Initialize()
        {
            //add timekit app headers
           Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
             "Basic",
              Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(
                        string.Format("{0}:{1}", azureUser.email, azureUser.api_token))));

            Client.DefaultRequestHeaders.Add("Timekit-App", TIMEKIT_APP_ID);
            //Client.DefaultRequestHeaders.Add("Content-Type", "application/json");

        }

        public TimeKitController()
        {
           
            TIMEKIT_API_FINDTIME_URL = $"{TIMEKIT_API_BASE_URL}findtime";
            TIMEKIT_API_USERS_URL = $"{TIMEKIT_API_BASE_URL}users";
            TIMEKIT_APP_ID = "renehealth";
        }


        public async Task<TimekitTimesResponse> GetAllTimesAsync(string email1, string email2, string timezone1, string timezone2, string length, int days = 4)
        {

            TimekitTimesResponse returnValue = new TimekitTimesResponse();

            var payload =
                new JObject(
                    new JProperty("emails", new JArray(email1,email2)),
                    new JProperty("filters", 
                        new JObject(
                            new JProperty("and",
                                new JArray(
                                    Filters.daytimeFilter(timezone2),
                                    Filters.businessHoursFilter(timezone1),
                                    Filters.excludeWeekendFilter()
                                )
                            )
                        )
                    ),
                    new JProperty("future", $"{days} days"),
                    new JProperty("length",length)
                );
            string ex = JToken.Parse(payload.ToString()).ToString();

            var content = new StringContent(payload.ToString(),System.Text.Encoding.UTF8,"application/json");
                       
            try
            {
                var resp = await Client.PostAsync(TIMEKIT_API_FINDTIME_URL,content);
                var respstring = await resp.Content.ReadAsStringAsync();

                var timestoreturn = JsonConvert.DeserializeObject<TimekitTimes>(respstring);

                returnValue.RootObject = timestoreturn;
                returnValue.Success = timestoreturn != null;
            }
            catch (JsonSerializationException jsonException)
            {
                returnValue.ErrorMessage = jsonException.Message;
                returnValue.Success = false;
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.Message.Contains("401"))
                    returnValue.ErrorMessage = "The client was not authorized. Did you call ServicesInitializer.Initialize()?";
                else if (httpException.Message.Contains("406"))
                    returnValue.ErrorMessage = "The client was authorized, but the request did not have the proper headers fixed. Did you call ServicesInitializer.Initialize()?";
                else if (httpException.Message.Contains("501"))
                    returnValue.ErrorMessage = "The request URL was bad.";
                else
                    returnValue.ErrorMessage = httpException.Message;

                returnValue.Success = false;
            }
            catch (Exception e)
            {
                returnValue.ErrorMessage = e.Message;
                returnValue.Success = false;
            }

            return returnValue;
        }

        public async Task<TimekitUserResponse> CreateUserAsync(string email, string timezone, string firstname, string lastname)
        {

            TimekitUserResponse returnValue = new TimekitUserResponse();

            var payload =
                new JObject(
                    new JProperty("email", email),
                    new JProperty("timezone", timezone),
                    new JProperty("first_name", firstname),
                    new JProperty("last_name", lastname)
                );
            string ex = JToken.Parse(payload.ToString()).ToString();

            var content = new StringContent(payload.ToString(), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var resp = await Client.PostAsync(TIMEKIT_API_USERS_URL, content);
                var respstring = await resp.Content.ReadAsStringAsync();
                var substring = JObject.Parse(respstring).SelectToken("data");
                var valstoreturn = JsonConvert.DeserializeObject<TimekitUser>(substring.ToString());

                returnValue.RootObject = valstoreturn;
                returnValue.Success = valstoreturn != null;
            }
            catch (JsonSerializationException jsonException)
            {
                returnValue.ErrorMessage = jsonException.Message;
                returnValue.Success = false;
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.Message.Contains("401"))
                    returnValue.ErrorMessage = "The client was not authorized. Did you call ServicesInitializer.Initialize()?";
                else if (httpException.Message.Contains("406"))
                    returnValue.ErrorMessage = "The client was authorized, but the request did not have the proper headers fixed. Did you call ServicesInitializer.Initialize()?";
                else if (httpException.Message.Contains("501"))
                    returnValue.ErrorMessage = "The request URL was bad.";
                else if (httpException.Message.Contains("422"))
                    returnValue.ErrorMessage = "Unproccesable Entity";
                else
                    returnValue.ErrorMessage = httpException.Message;

                returnValue.Success = false;
            }
            catch (Exception e)
            {
                returnValue.ErrorMessage = e.Message;
                returnValue.Success = false;
            }

            return returnValue;
        }

    }
}
