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
    
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new {message = "Email i hasło są wymagane"});
            }

            var user = await _loginService.AuthenticateUserAsync(request.Email, request.Password);

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
    }
}