using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient HttpClient { get; set; }
        private readonly string _uriAction = "Spel";
        private readonly ReversiDbContext _reversiDbContext;
        private readonly ApplicationDbContext _context;

        public HomeController(ReversiDbContext reversiDbContext, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            HttpClient = httpClientFactory.CreateClient("reversiClient");
            _reversiDbContext = reversiDbContext;
            _context = context;
        }

        public IActionResult Index()
        {
            if(User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var currentUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                RegistreerSpeler(currentUserID);

                var response = HttpClient.GetAsync($"{ _uriAction}/SpelSpeler/{currentUserID}");
                if (response.Result != null && response.Result.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Spel>(response.Result.Content.ReadAsStringAsync().Result);
                    return View("../Spel/Details", result);
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void RegistreerSpeler(Guid currentUserID)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                if (_reversiDbContext.Speler.Find(currentUserID) == null)
                {
                    var user = _context.Users.Find(currentUserID.ToString());
                    var speler = new Speler
                    {
                        ID = currentUserID,
                        Naam = user.UserName,
                    };
                    _reversiDbContext.Speler.Add(speler);
                    _reversiDbContext.SaveChanges();
                }
            }
        }
    }
}
