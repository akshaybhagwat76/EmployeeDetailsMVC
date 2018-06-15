using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        protected int UserId
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                       .Select(c => c.Value).SingleOrDefault());
                return userId;
            }
        }
    }
}