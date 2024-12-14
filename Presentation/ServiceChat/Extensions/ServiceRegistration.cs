namespace ServiceChat.WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddServiceClient<PetSocialNetwork.ServiceUser.IUserProfileClient, PetSocialNetwork.ServiceUser.UserProfileClient>("UserService");

            return services;
        }
    }
}
