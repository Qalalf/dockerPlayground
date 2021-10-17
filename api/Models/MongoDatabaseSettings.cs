namespace api.Models
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string TestCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }

    public interface IMongoDatabaseSettings
    {
        string TestCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}