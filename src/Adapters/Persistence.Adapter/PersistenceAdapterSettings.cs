namespace Persistence.Adapter
{
    public sealed class PersistenceAdapterSettings
    {
        public string AccountEndpoint { get; }
        public string AccountKey { get; }

        public PersistenceAdapterSettings(string accountEndpoint, string accountKey)
        {
            AccountEndpoint = accountEndpoint;
            AccountKey = accountKey;
        }
    }
}
