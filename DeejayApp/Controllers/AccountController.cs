using DeejayApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DeejayApp.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            using (var context = new DeejayContext())
            {
                var djusers = context.DeejayTB;

                if (djusers.Any(p => p.Email == model.UserName.Trim() && p.Password == model.Password.Trim()))
                {

                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, djusers.Where(p => p.Email == model.UserName.Trim() && p.Password == model.Password.Trim()).FirstOrDefault().FirstName.ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, djusers.Where(p => p.Email == model.UserName.Trim() && p.Password == model.Password.Trim()).FirstOrDefault().Id.ToString()));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult CreateDeejay(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeejay(Deejay model)
        {
            if (!ModelState.IsValid)
                return View();

            using (var context = new DeejayContext())
            {
                var djusers = context.DeejayTB;

                if (!context.DeejayTB.Any(x => x.Email == model.Email.Trim()))
                {
                    model.FirstName = model.FirstName.Trim();
                    model.LastName = model.LastName.Trim();
                    model.Email = model.Email.Trim();
                    model.Password = model.Password.Trim();

                    context.DeejayTB.Add(model);
                    context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Deejay already exists.");
                    return View(model);
                }
            }

        }

    }
}