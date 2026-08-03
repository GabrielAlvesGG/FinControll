using Microsoft.AspNetCore.Mvc;

namespace FinControllAPI.Controllers;

[Route("api/Register")]
public class RegisterController : Controller
{
    [HttpPost("/api/Register/Teste")]
    public IActionResult RegisterInstitution()
    { 
        return Ok("Teste");
    }
}