using System.Net;
using System.Net.Mime;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace _Kurs_webb_csharp.Controllers;

[ApiController]
[Route("/api/messages")]
public class Messages(List<Message> messageHistorySingleton) : Controller
{
    private readonly List<Message> _messageHistorySingleton = messageHistorySingleton;

    [HttpPost]
    public IActionResult Create([FromBody] Message message)
    {
        if (message.MessageSent == null)
        {
            return BadRequest("MessageSent cannot be null.");
        }

        Message newMessage = new Message { User = message.User, MessageSent = message.MessageSent };
        _messageHistorySingleton.Add(newMessage);

        // write the message to the bottom of the file
        Console.WriteLine($"Received message: {message.User}");
        return Ok();
    }

    [HttpGet]
    public IActionResult GetMessages()
    {
        return Ok(_messageHistorySingleton);
    }
}
