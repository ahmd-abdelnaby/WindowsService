using Microsoft.AspNetCore.Mvc;
using Repository;
using Service;
using Service.ViewModels;
using System;
using System.Linq;
using VictoryAPI.Models;

namespace VictoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestService RequestService;
        public RequestsController( RequestService RequestService)
        {
            this.RequestService = RequestService;
        }
        [HttpPost]
        [Route("AddRequest")]
        public Response AddRequest(RequestVM request)
        {
            var MobleExist = RequestService.MobileExist(request.MobileNumber);
            if (MobleExist)
                return new Response { Status = 2, Message = "MobileNumber was added before" };
            else
            {
                var AffectedRows = RequestService.AddRequest(request);
                if (AffectedRows>0)
                    return new Response { Status = 1, Message = "Request Is Added " };
                return new Response { Status = 3, Message = "Failed" };
            }
        }
    }
}
