using Microsoft.AspNetCore.Identity;
using ReversiMvcApp.Data;
using ReversiMvcApp.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ReversiMvcApp.Logic
{
    public class UserLogic : IUserLogic
    {
        private ReversiDbContext _context;

        public UserLogic(ReversiDbContext context)
        {
            _context = context;
        }

        public List<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetByID(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
