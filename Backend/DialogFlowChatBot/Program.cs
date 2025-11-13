using DialogFlowChatBot.DTOs;
using DialogFlowChatBot.Services;
using Microsoft.AspNetCore.WebSockets;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDialogFlowService, DialogFlowService>();
builder.Services.AddScoped<WebSocketService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseWebSockets();

app.Map("/ws/chat", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    var handler = context.RequestServices.GetRequiredService<WebSocketService>();
    await handler.HandleChatAsync(webSocket);
});


app.Run();
