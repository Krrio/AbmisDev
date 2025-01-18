using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Models;

namespace backend.Services
{
    public interface ILoginService
    {
        Task<User?> AuthenticateUserAsync(string email, string password);
    }
}