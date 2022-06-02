using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using ReversiMvcApp.Models.Enums;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReversiMvcApp.Hubs
{
    public class ReversiHub : Hub
    {
        private HttpClient HttpClient { get; set; }
        private readonly string _spelAction = "Spel";
        private readonly string _uitslagAction = "uitslag";
        private readonly ReversiDbContext _context;

        public ReversiHub(IHttpClientFactory httpClientFactory, ReversiDbContext context)
        {
            HttpClient = httpClientFactory.CreateClient("reversiClient");
            _context = context;
        }

        public async Task MakeMove(Guid token, string spelerToken, int y, int x)
        {
            if (!await ValidateTurn(token, spelerToken))
            {
                return;
            }
            var json = CreateRequest(token, y, x);

            var response = await HttpClient.PutAsync($"{_spelAction}/Zet", json);
            if (response.IsSuccessStatusCode)
            {
                await Clients.Group(token.ToString()).SendAsync("MoveFinished", HttpClient.GetAsync($"{_spelAction}/beurtkleur/{token}").Result.Content.ReadAsStringAsync().Result);
                await CheckGameState(token);
                return;
            }

            // niet jouw beurt
        }

        public async Task Forfeit(Guid token, string spelerToken)
        {
            var json = new StringContent(JsonConvert.SerializeObject(new { token = token, opgegeven = true, opgever = spelerToken }), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{_uitslagAction}", json);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<Uitslag>(response.Content.ReadAsStringAsync().Result);
                await Clients.Group(token.ToString()).SendAsync("GameFinished", result);

                var speler1 = await _context.Speler.FindAsync(new Guid(result.Speler1Token));
                var speler2 = await _context.Speler.FindAsync(new Guid(result.Speler2Token));

                if (result.Winnaar == Kleur.Wit)
                {
                    speler1.AantalGewonnen++;
                    speler2.AantalVerloren++;
                }
                else
                {
                    speler1.AantalVerloren++;
                    speler2.AantalGewonnen++;
                }
                _context.Update(speler1);
                _context.Update(speler2);
                _context.SaveChanges();
            }
        }

        public async Task Pass(Guid token, string spelerToken)
        {
            if (!await ValidateTurn(token, spelerToken))
            {
                await Clients.Caller.SendAsync("PassFinished", false, "Passen niet mogelijk, niet jouw beurt");
                return;
            }
            var json = new StringContent(JsonConvert.SerializeObject(new { token = token }), Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync($"{_spelAction}/Pas", json);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                if (result)
                {
                    await Clients.Group(token.ToString()).SendAsync("PassFinished", result, "Gelukt");
                }
                else
                {
                    await Clients.Caller.SendAsync("PassFinished", result, "Passen niet mogelijk, nog een beurt mogelijk");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("PassFinished", false, "Iets is fout gegaan");
            }
        }

        private async Task<bool> ValidateTurn(Guid token, string spelerToken)
        {
            var getBeurt = await HttpClient.GetAsync($"{_spelAction}/beurt/{token}");
            if (getBeurt.IsSuccessStatusCode)
            {
                var beurt = getBeurt.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(beurt))
                {
                    return false;
                }
                else if (beurt != spelerToken)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task CheckGameState(Guid token)
        {
            var getSpel = await HttpClient.GetAsync($"{_spelAction}/{token}");
            var spel = JsonConvert.DeserializeObject<Spel>(getSpel.Content.ReadAsStringAsync().Result);

            if (spel.Status != Status.Afgelopen)
                return;

            var json = new StringContent(JsonConvert.SerializeObject(new { token = token }), Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync($"{_uitslagAction}", json).Result;
            var result = JsonConvert.DeserializeObject<Uitslag>(response.Content.ReadAsStringAsync().Result);

            var speler1 = await _context.Speler.FindAsync(new Guid(result.Speler1Token));
            var speler2 = await _context.Speler.FindAsync(new Guid(result.Speler2Token));

            if (result.Winnaar == Kleur.Wit)
            {
                speler1.AantalGewonnen++;
                speler2.AantalVerloren++;
            }
            else if (result.Winnaar == Kleur.Zwart)
            {
                speler1.AantalVerloren++;
                speler2.AantalGewonnen++;
            }
            else
            {
                speler1.AantalGelijk++;
                speler2.AantalGelijk++;
            }
            _context.Update(speler1);
            _context.Update(speler2);
            _context.SaveChanges();

            await Clients.Group(spel.ID.ToString()).SendAsync("GameFinished", result);
        }

        public async Task JoinGroup(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

        private StringContent CreateRequest(Guid token, int y, int x)
        {
            return new StringContent(JsonConvert.SerializeObject(new
            {
                Token = token,
                X = x,
                Y = y
            }), Encoding.UTF8, "application/json");
        }
    }
}
