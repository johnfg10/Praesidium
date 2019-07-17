namespace Praesidium.Models
{
    public class MongoOptions
    {
        public MongoOptions(string mongoDbConnectionString, string databaseName)
        {
            MongoDbConnectionString = mongoDbConnectionString;
            DatabaseName = databaseName;
        }

        public string MongoDbConnectionString { get; set; }
        
        public string DatabaseName { get; set; }
    }
}