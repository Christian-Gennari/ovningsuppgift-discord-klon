using Microsoft.AspNetCore.Mvc;

namespace _Kurs_webb_csharp.Controllers;

[ApiController]
[Route("/api/messages")]
public class Messages(
    List<Message> messageHistorySingleton,
    IHostApplicationLifetime lifetime,
    MessageNotifier notifier
) : Controller
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

        notifier.Notify();
        Console.WriteLine($"Received message: {message.User}");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        Request.Headers.TryGetValue("x-poll", out var pollHeader);

        if (pollHeader == "yes")
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(
                HttpContext.RequestAborted,
                lifetime.ApplicationStopping
            );
            try
            {
                await notifier.WaitForNewMessage(cts.Token);
            }
            catch (OperationCanceledException) { }
        }

        return Ok(_messageHistorySingleton);
    }
}
