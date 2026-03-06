namespace _Kurs_webb_csharp;

public class MessageNotifier
{
    private readonly Lock _lock = new();
    private TaskCompletionSource tcs = new();
    public Task WaitForNewMessage(CancellationToken ct) => tcs.Task.WaitAsync(ct);

    public void Notify()
    {
        lock (_lock)
        {
            tcs.TrySetResult();
            tcs = new TaskCompletionSource();
        }
    }
}
