using Calculator.BusinessLogic.Models;
using MongoDB.Driver;

namespace Calculator.BusinessLogic.Repositories.Core
{
    public class BaseRepository
    {
        protected readonly IMongoDatabase m_mongoDatabase;
        public BaseRepository(ICalculatorDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            m_mongoDatabase = client.GetDatabase(settings.DatabaseName);
        }
    }
}
