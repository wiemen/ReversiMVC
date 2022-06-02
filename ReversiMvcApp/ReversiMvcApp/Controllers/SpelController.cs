using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReversiMvcApp.Controllers
{
    public class SpelController : Controller
    {
        private HttpClient HttpClient { get; set; }
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly string _uriAction = "Spel";

        public SpelController(IHttpClientFactory httpClientFactory, ApplicationDbContext applicationDBContext)
        {
            HttpClient = httpClientFactory.CreateClient("reversiClient");
            _applicationDBContext = applicationDBContext;
        }

        // GET: SpelController
        [Authorize(Roles = "Speler")]
        public ActionResult Index()
        {
            var spel = GetSpelSpeler();
            if (spel != null)
            {
                return View("Details", spel);
            }

            var response = HttpClient.GetAsync(_uriAction).Result;
            var result = JsonConvert.DeserializeObject<List<Spel>>(response.Content.ReadAsStringAsync().Result);
            var users = _applicationDBContext.Users;
            result.ForEach(x => { x.Speler1Naam = users.Find(x.Speler1Token).UserName; });

            return View(result);
        }

        [Authorize(Roles = "Speler")]
        // GET: SpelController/Details/5
        public ActionResult Details(Guid id)
        {
            var response = HttpClient.GetAsync($"{_uriAction}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<Spel>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }

            return View();
        }

        [Route("[action]/{spel}")]
        public ActionResult Details(Spel spel)
        {
            return View(spel);
        }

        // GET: SpelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spel spel)
        {
            try
            {
                spel.Speler1Token = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var json = new StringContent(JsonConvert.SerializeObject(spel), Encoding.UTF8, "application/json");

                var response = HttpClient.PostAsync($"{_uriAction}", json).Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Join(Guid? spelId)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(new
                {
                    Token = spelId.ToString(),
                    Speler2Token = User.FindFirst(ClaimTypes.NameIdentifier).Value
                }), Encoding.UTF8, "application/json");

                var response = HttpClient.PutAsync($"{_uriAction}/Join", json).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Spel>(response.Content.ReadAsStringAsync().Result);
                    return View("Details", result);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        private Spel GetSpelSpeler()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var response = HttpClient.GetAsync($"{_uriAction}/SpelSpeler/{User.FindFirst(ClaimTypes.NameIdentifier).Value}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Spel>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return null;
        }
    }
}
