// using System.Net;
// using System.Net.Mime;
// using System.Runtime.Versioning;
// using System.Security.Cryptography;
// using System.Security.Cryptography.X509Certificates;
// using Microsoft.AspNetCore.Mvc;

// namespace _Kurs_webb_csharp.Controllers;

// [ApiController]
// [Route("/")]
// [Route("home")]
// public class Home : Controller
// {
//     [HttpGet]
//     public IActionResult Get()
//     {
//         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "public", "index.html");

//         if (!System.IO.File.Exists(filePath))
//         {
//             return NotFound("The file public/index.html was not found.");
//         }
//         return PhysicalFile(filePath, "text/html");
//     }
// }
