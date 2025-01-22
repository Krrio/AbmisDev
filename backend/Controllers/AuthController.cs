using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
       private readonly ILoginService _loginService;
       private readonly IRegisterService _registerService;
    
        public AuthController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Sprawdzamy czy użytkownik nie wprowadza pustych danych
            if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new {message = "Email i hasło są wymagane"});
            }

            var user = await _loginService.AuthenticateUserAsync(request);
        
            if(user == null){
                return Unauthorized(new {message = "Nieprawidłowy email lub hasło"});
            }

            var token = _loginService.GenerateJwtToken(user);

            return Ok(new 
            {
                message = "Logowanie powiodło się!",
                token
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Sprawdzamy czy użytkownik nie wprowadza pustych danych
            if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest(new {message = "Email, hasło i potwierdzenie hasła są wymagane!"});
            }
            // Sprawdzamy czy hasła są takie same
            if(request.Password != request.ConfirmPassword)
            {
                return BadRequest(new {message = "Hasła muszą być takie same!"});
            }
            try{
                await _registerService.RegisterUserAsync(request);
                return Ok(new {message = "Rejestracja powiodła się!"});
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }

        }
    }
}