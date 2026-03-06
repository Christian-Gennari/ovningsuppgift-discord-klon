using System.ComponentModel.Design.Serialization;
using System.Net;
using System.Net.Cache;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace _Kurs_webb_csharp.Controllers;

[ApiController]
[Route("/api/messages")]
public class Messages(List<Message> messageHistorySingleton, IHostApplicationLifetime lifetime)
    : Controller
{
    private readonly List<Message> _messageHistorySingleton;

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
    public async Task<IActionResult> GetMessages()
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(
            HttpContext.RequestAborted,
            lifetime.ApplicationStopping
        );

        Request.Headers.TryGetValue("x-poll", out var pollHeader);

        if (pollHeader == "yes")
        {
            try
            {
                await Task.Delay(3000, cts.Token);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Long polling request was cancelled.");
            }

            return Ok(_messageHistorySingleton);
        }
        else
        {
            return Ok(_messageHistorySingleton);
        }
    }
}
