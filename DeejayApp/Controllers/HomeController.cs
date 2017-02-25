using System.Web.Mvc;
using DeejayApp.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System;
using System.Net.Http.Headers;

namespace DeejayApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Deejay NewDj = new Deejay { FirstName = "FN", LastName = "LN", Email = "admin@admin.com", Password = "12345" };

            using (var context = new DeejayContext())
            {
                if (!context.DeejayTB.Any(x => x.Email == "admin@admin.com"))
                {
                    context.DeejayTB.Add(NewDj);
                    context.SaveChanges();
                }
            }

            

            return View();
        }

        public ActionResult ResonseFromApi()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50583");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/PlaylistsAPI").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    ViewBag.ResonseFromApi = responseString;
                }
            }



            return View();
        }


    }
}