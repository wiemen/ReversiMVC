using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using System;
using System.Threading.Tasks;

namespace ReversiMvcApp.Controllers
{
    [Authorize(Roles = "Mediator, Beheerder")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly ReversiDbContext _reversiDBContext;

        public UserController(ApplicationDbContext applicationDBContext, ReversiDbContext reversiDBContext)
        {
            _applicationDBContext = applicationDBContext;
            _reversiDBContext = reversiDBContext;
        }

        public IActionResult Index()
        {
            return View(_applicationDBContext.Users);
        }

        // GET: Spelers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _applicationDBContext.Users
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Spelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _applicationDBContext.Users.FindAsync(id.ToString());
            _applicationDBContext.Users.Remove(user);
            await _applicationDBContext.SaveChangesAsync();

            try
            {
                var speler = await _reversiDBContext.Speler.FindAsync(id);
                _applicationDBContext.Users.Remove(user);
                await _reversiDBContext.SaveChangesAsync();
            }
            catch (Exception) { }
            

            return RedirectToAction(nameof(Index));
        }
    }
}
