using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using ReversiMvcApp.Models.DTO;
using ReversiMvcApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ReversiMvcApp.Controllers
{
    public class SpelController : Controller
    {
        private ISpelLogic _spelLogic;
        private IUserLogic _userLogic;

        public SpelController(ISpelLogic spelLogic, IUserLogic userLogic)
        {
            _spelLogic = spelLogic;
            _userLogic = userLogic;
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

            var spellen = _spelLogic.GetWachtendeSpellen();
            var users = _userLogic.GetAll();
            spellen.ForEach(x => { x.Speler1Naam = users.Find(s=>s.Id == x.Speler1Token).UserName; });

            return View(spellen);
        }

        [Authorize(Roles = "Speler")]
        // GET: SpelController/Details/5
        public ActionResult Details(string id)
        {
            var spel = _spelLogic.Get(id);
            return View(spel);
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
            spel.Speler1Token = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _spelLogic.Add(spel) ? RedirectToAction(nameof(Index)) : View();
        }

        public ActionResult Join(Guid? spelId)
        {
            if(spelId == null)
            {
                return null;
            }

            try
            {
                var spel = _spelLogic.Join(spelId.Value, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (spel != null)
                {
                    return View("Details", spel);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //API controllers

        [HttpGet]
        [Route("api/Spel")]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            var result = _spelLogic.GetAll().Where(x => string.IsNullOrEmpty(x.Speler2Token));
            return Ok(result.Select(x => new { x.ID, x.Omschrijving, x.Speler1Token }));
        }

        [Route("api/Spel/{token}")]
        //[Route("{token}")]
        [HttpGet]
        public ActionResult GetByToken(string token)
        {
            var spel = _spelLogic.Get(token);
            if (spel == null)
            {
                return BadRequest();
            }

            return Ok(GetJson(spel));
        }

        [Route("api/Spel/[action]")]
        //[Route("[action]")]
        [HttpPut]
        public ActionResult<bool> Pas([FromBody] SpelPutDTO request)
        {
            var spel = _spelLogic.Get(request.Token);
            if (spel == null)
            {
                return BadRequest();
            }

            try
            {
                spel.Pas();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Route("api/Spel/[action]/{token}")]
        //[Route("[action]/{token}")]
        [HttpGet]
        public ActionResult<List<(int, int, int)>> GetStats(string token)
        {
            var spel = _spelLogic.Get(token);
            if (spel == null)
            {
                return BadRequest();
            }

            return Ok(GetJson(spel.Beurten));
        }

        [Route("api/Spel/[action]/{token}")]
        //[Route("[action]/{token}")]
        [HttpGet]
        public ActionResult SpelSpeler(string token)
        {
            var spel = _spelLogic.GetAll().FirstOrDefault(x => (x.Speler1Token == token || x.Speler2Token == token) &&
            (x.Status == Status.Bezig || x.Status == Status.Wachtende));
            if (spel == null)
            {
                return BadRequest();
            }
            return Ok(GetJson(spel));
        }

        [Route("api/Spel/[action]/{token}")]
        //[Route("[action]/{token}")]
        [HttpGet]
        public ActionResult<string> Beurt(string token)
        {
            var spel = _spelLogic.Get(token);
            if (spel == null)
            {
                return BadRequest();
            }

            if (spel.AandeBeurt == Kleur.Wit)
            {
                return spel.Speler1Token;
            }
            else if (spel.AandeBeurt == Kleur.Zwart)
            {
                return spel.Speler2Token;
            }
            else
            {
                return string.Empty;
            }
        }

        [Route("api/Spel/[action]/{token}")]
        //[Route("[action]/{token}")]
        [HttpGet]
        public ActionResult<Kleur> BeurtKleur(string token)
        {
            var spel = _spelLogic.Get(token);
            if (spel == null)
            {
                return BadRequest();
            }

            return spel.AandeBeurt;
        }

        [Route("[action]")]
        [Route("api/Spel/[action]")]
        [HttpPut]
        public ActionResult<bool> Zet([FromBody] SpelPutDTO request)
        {
            var spel = _spelLogic.Get(request.Token);
            if (spel == null)
            {
                return BadRequest();
            }

            spel.DoeZet(request.X.Value, request.Y.Value);

            if (spel.Afgelopen())
                spel.Status = Status.Afgelopen;
            return Ok(_spelLogic.Update(spel));
        }

        private string GetJson(object spel)
        {
            return JsonConvert.SerializeObject(spel, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private Spel GetSpelSpeler()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                return _spelLogic.GetBySpelerID(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }

            return null;
        }
    }
}
