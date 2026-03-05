var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add global singleton list of messages so it is easily accessible in the Messages controller
builder.Services.AddSingleton<List<Message>>(
    new List<Message>
    {
        new Message { User = "Admin", MessageSent = "Welcome to the chat!" },
        new Message { User = "Admin", MessageSent = "Feel free to send a message." },
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
