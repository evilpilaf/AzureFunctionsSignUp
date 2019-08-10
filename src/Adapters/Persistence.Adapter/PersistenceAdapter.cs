using Microsoft.Extensions.DependencyInjection;

using SignUp.core.Ports;

using SimpleInjector;

namespace Persistence.Adapter
{
    public sealed class PersistenceAdapter
    {
        private readonly Container _container;

        public PersistenceAdapter()
        {
            _container = new Container();
            _container.Register<ICourseLoader, CourseLoaderInMemory>();
        }

        public void Register(IServiceCollection hostContainer)
        {
            hostContainer.AddScoped<ICourseLoader>(_ => _container.GetInstance<ICourseLoader>());
        }
    }
}
