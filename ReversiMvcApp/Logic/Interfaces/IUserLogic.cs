using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ReversiMvcApp.Logic.Interfaces
{
    public interface IUserLogic
    {
        List<IdentityUser> GetAll();
        IdentityUser GetByID(string id);
    }
}
