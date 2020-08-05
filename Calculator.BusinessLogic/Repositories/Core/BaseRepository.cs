using MongoDB.Driver;

namespace Calculator.BusinessLogic.Repositories.Core
{
    public class BaseRepository
    {
        protected readonly IMongoDatabase m_mongoDatabase;
        public BaseRepository()
        {
            string connectionString = "mongodb://localhost:27017/mobilestore";
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            m_mongoDatabase = client.GetDatabase(connection.DatabaseName);
        }
    }
}
