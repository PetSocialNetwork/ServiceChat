using Microsoft.EntityFrameworkCore;
using ServiceChat.DataEntityFramework;
using ServiceChat.DataEntityFramework.Repositories;
using ServiceChat.Domain.Interfaces;
using ServiceChat.Domain.Services;
using ServiceChat.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHub<ChatHub>("/chatHub");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
