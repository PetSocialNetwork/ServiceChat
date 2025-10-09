using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceChat.DataEntityFramework.Repositories;
using ServiceChat.DataEntityFramework;
using ServiceChat.Domain.Interfaces;
using ServiceChat.WebApi.Filters;
using ServiceChat.Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using ServiceChat.WebApi.Hubs;

namespace ServiceChat.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure();
            services.AddApplicationComponents(configuration);
        }

        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseWebSockets();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }

        private static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
            services.AddControllers(options =>
            {
                options.Filters.Add<CentralizedExceptionHandlingFilter>();
            });

            services.AddFluentValidationAutoValidation();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed(_ => true)
                          .AllowCredentials();
                });
            });

            services.AddSignalR();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatService", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        private static void AddApplicationComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddDomainServices();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
        }

        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<MessageService>();
            services.AddScoped<ChatService>();
        }

        public static void AddServiceClient<TClientInterface, TClientImplementation>(
            this IServiceCollection services,
            string serviceConfigKey)
            where TClientImplementation : class, TClientInterface
            where TClientInterface : class
        {
            services.AddScoped(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                var configuration = provider.GetRequiredService<IConfiguration>();
                var baseAddress = configuration.GetValue<string>($"Services:{serviceConfigKey}")
                                  ?? throw new InvalidOperationException($"Configuration value for Services:{serviceConfigKey} not found.");


                var constructor = typeof(TClientImplementation).GetConstructor([typeof(string), typeof(HttpClient)]);

                if (constructor == null)
                {
                    throw new InvalidOperationException($"Type {typeof(TClientImplementation).FullName} must have a constructor accepting a string and HttpClient.");
                }

                return (TClientInterface)constructor.Invoke([baseAddress, httpClient]);

            });
        }
    }
}
