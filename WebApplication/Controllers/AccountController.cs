using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication.ViewModel;
using WebApplication.Repository;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                       .Select(c => c.Value).SingleOrDefault());
                var user = new UserRepository().GetProfile(userId);
                if (Request.IsAuthenticated && userId > 0 && user != null)
                {
                    return Redirect(returnUrl ?? "/Home/Index");
                }
                else
                {
                    return View();
                }
                ViewBag.returnUrl = returnUrl;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SignInUser(string useremail, string userid,string role_name, bool isPersistent)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Sid, userid));
            claims.Add(new Claim(ClaimTypes.Email, useremail));
            claims.Add(new Claim("userName",useremail));
            claims.Add(new Claim(ClaimTypes.Role, role_name));

            var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            // Sign In.    
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
        }
    
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserVM model, string returnUrl)
        {
            try
            {
                var user = new UserRepository().Login(model);
                SignInUser(user.UserName,user.RoleName, user.UserId.ToString(), false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var auth = ctx.Authentication;
            auth.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}