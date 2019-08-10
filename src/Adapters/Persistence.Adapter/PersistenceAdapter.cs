using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

using SignUp.core.Ports;

using Container = SimpleInjector.Container;

namespace Persistence.Adapter
{
    public sealed class PersistenceAdapter
    {
        private readonly Container _container;

        public PersistenceAdapter(PersistenceAdapterSettings settings)
        {
            _container = new Container();
            _container.Register<CosmosClient>(() =>
                new CosmosClient(settings.AccountEndpoint, settings.AccountKey));
            _container.Register<ICourseLoader, CourseLoaderCosmosDb>();
        }

        public void Register(IServiceCollection hostContainer)
        {
            hostContainer.AddScoped(_ => _container.GetInstance<ICourseLoader>());
        }
    }
}
