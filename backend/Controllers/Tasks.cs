using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class Tasks : Controller
    {
       [HttpGet]
       public IActionResult GetTasks()
       {
           return Ok(new {message = "Pomyślnie pobrano zadania"});

           
       }
    }
}