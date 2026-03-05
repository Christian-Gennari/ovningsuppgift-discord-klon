using System.Net;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace _Kurs_webb_csharp.Controllers;

[ApiController]
[Route("[controller]")]
public class Home : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<h1>Hello World!</h1>", "text/html");
    }
}
