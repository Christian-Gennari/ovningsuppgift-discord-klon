using System.Net;
using System.Net.Mime;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _Kurs_webb_csharp.Controllers;

[ApiController]
[Route("/api/messages")]
public class Messages : Controller
{
    /* [HttpPost]
    public IActionResult Create([FromBody] Message message )
    {
     

    } */

    [HttpGet]
    public IActionResult GetMessages()
    {
        var json = System.IO.File.ReadAllText("messages.json");

        var messages = JsonSerializer.Deserialize<List<Message>>(json);

        return Ok(messages);
    }
}

