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
    [MobileAppController]
    [RoutePrefix("api/v0.1")]
    public class TimekitUserController : ApiController
    {
        renehealthContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            context = new renehealthContext();
            
        }

        // GET api/time/v0.1
        [HttpGet, Route("createuser")]
        public async Task<string> Get(string email,string firstname,string lastname,string timezone)
        {
            var result = await TimeKitController.Instance.CreateUserAsync(email, timezone, firstname, lastname);

            if (result.RootObject != null)
            {
                //Create a new EntityData instance with the tag received from Intercom
                var newUser = new TimeKitUser(result.RootObject);

                //1. Get tag from DB in connected context in case we need to update
                var originalEntity = await context.TimeKitUsers.FindAsync(newUser.email,newUser.first_name);

                //2. Check to see if we found an existing entry, otherwise skip to 5
                if (originalEntity != null)
                {
                    //3. Update our new EntityData instance with original record
                    //newUser.UpdateEntityData(originalEntity);

                    //4. Set the values on the original entity
                    context.Entry(originalEntity).CurrentValues.SetValues(newUser);
                }
                else
                {
                    //5. Add new Tag to database
                    context.TimeKitUsers.Add(newUser);
                }

                //6. Save database context
                await context.SaveChangesAsync();

                return "SUCCESS";
            }

            return "FAILED";
        }
    }
}
