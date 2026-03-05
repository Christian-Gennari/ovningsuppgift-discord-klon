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
public class Messages : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] Message message)
    {
        if (message.MessageSent == null)
        {
            return BadRequest("MessageSent cannot be null.");
        }
        // Get the file of messages.json
        var filePath = "messages.json";

        Message newMessage = new Message { User = message.User, MessageSent = message.MessageSent };

        var json = System.IO.File.ReadAllText(filePath);
        // Read the file and deserialize it to a list of messages

        var messages = JsonSerializer.Deserialize<List<Message>>(json);

        messages.Add(newMessage);
        // Serialize the list of messages and write it to the file
        var updatedJson = JsonSerializer.Serialize(messages);
        System.IO.File.WriteAllText(filePath, updatedJson);

        // write the message to the bottom of the file
        Console.WriteLine($"Received message: {message.User}");
        return Ok();
    }

    [HttpGet]
    public IActionResult GetMessages()
    {
        var json = System.IO.File.ReadAllText("messages.json");

        var messages = JsonSerializer.Deserialize<List<Message>>(json);

        return Ok(messages);
    }
}
