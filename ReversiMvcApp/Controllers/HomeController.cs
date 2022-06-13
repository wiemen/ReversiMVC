using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private ISpelLogic _spelLogic;
        private IUserLogic _userLogic;
        private ISpelerLogic _spelerLogic;

        public HomeController(ISpelLogic spelLogic, IUserLogic userLogic, ISpelerLogic spelerLogic)
        {
            _spelLogic = spelLogic;
            _userLogic = userLogic;
            _spelerLogic = spelerLogic;
        }

        public IActionResult Index()
        {
            Spel spel = null;

            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var currentUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                RegistreerSpeler(currentUserID);
                spel = _spelLogic.GetBySpelerID(currentUserID.ToString());
            }

            return spel != null ? View("../Spel/Details", spel) : View();
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
                if (_spelerLogic.GetByID(currentUserID) == null)
                {
                    var user = _userLogic.GetByID(currentUserID.ToString());
                    var speler = new Speler
                    {
                        ID = currentUserID,
                        Naam = user.UserName,
                    };

                    _spelerLogic.Add(speler);
                }
            }
        }
    }
}
