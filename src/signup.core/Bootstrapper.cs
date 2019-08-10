using Microsoft.Extensions.DependencyInjection;

namespace SignUp.core
{
    public static class Bootstrapper
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SignUpToCourseV1UseCase>();
            serviceCollection.AddScoped<GetAllCoursesUseCase>();
        }
    }
}
