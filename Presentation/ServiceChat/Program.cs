using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceChat.DataEntityFramework;
using ServiceChat.DataEntityFramework.Repositories;
using ServiceChat.Domain.Interfaces;
using ServiceChat.Domain.Services;
using ServiceChat.WebApi;
using ServiceChat.WebApi.Extensions;
using ServiceChat.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CentralizedExceptionHandlingFilter>();
});
builder.Services.AddClientServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatService", Version = "v1" });
    //options.UseAllOfToExtendReferenceSchemas();
    //string pathToXmlDocs = Path.Combine(AppContext.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".xml");
    //options.IncludeXmlComments(pathToXmlDocs, true);
});

builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient();
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
