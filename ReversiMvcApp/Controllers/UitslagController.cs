using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Models;
using ReversiMvcApp.Models.DTO;
using ReversiMvcApp.Models.Enums;
using System.Linq;

namespace ReversiMvcApp.Controllers
{
    [Route("api/Uitslag")]
    [ApiController]
    public class UitslagController : ControllerBase
    {
        private ISpelLogic _spelLogic;
        private IUitslagLogic _uitslagLogic;

        public UitslagController(ISpelLogic spelLogic, IUitslagLogic uitslagLogic)
        {
            _spelLogic = spelLogic;
            _uitslagLogic = uitslagLogic;
        }

        [HttpGet]
        public ActionResult Hoi()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<Uitslag> Post([FromBody] UitslagDTO request)
        {
            var spel = _spelLogic.Get(request.Token);
            if (spel == null)
            {
                return BadRequest();
            }
            else if (_uitslagLogic.GetAll().FirstOrDefault(x => x.SpelID.ToString() == request.Token) != null)
            {
                return BadRequest();
            }

            var uitslag = CreateUitslag(spel, request);
            spel.Status = Status.Afgelopen;
            _spelLogic.Update(spel);

            _uitslagLogic.Add(uitslag);
            return Ok(uitslag);
        }

        private Uitslag CreateUitslag(Spel spel, UitslagDTO request)
        {
            int aantalWit = 0;
            int aantalZwart = 0;
            for (int rijZet = 0; rijZet < 8; rijZet++)
            {
                for (int kolomZet = 0; kolomZet < 8; kolomZet++)
                {
                    if (spel.Bord[rijZet, kolomZet] == Kleur.Wit)
                        aantalWit++;
                    else if (spel.Bord[rijZet, kolomZet] == Kleur.Zwart)
                        aantalZwart++;
                }
            }

            var uitslag = new Uitslag(aantalWit, aantalZwart, spel, aantalWit > aantalZwart ? Kleur.Wit : Kleur.Zwart);
            if (request.Opgegeven)
            {
                if (spel.Speler1Token.Equals(request.Opgever))
                    uitslag.Winnaar = Kleur.Zwart;
                else
                    uitslag.Winnaar = Kleur.Wit;
            }
            else if (aantalWit == aantalZwart)
            {
                uitslag.Winnaar = Kleur.Geen;
            }

            return uitslag;
        }
    }
}
