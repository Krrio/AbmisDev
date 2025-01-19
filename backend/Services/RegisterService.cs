using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly AppDbContext _context;
        public RegisterService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(RegisterRequestDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(existingUser != null)
            {
                throw new Exception("Ten adres email jest już zajęty!");
            }
            var newUser = new User{
                Email = request.Email,
                Password = request.Password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            Console.WriteLine("Użytkownik zarejestrowany pomyślnie!");

            return true;
        }
    }
}