using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BusinessLogic.Contracts;
using Calculator.BusinessLogic.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Calculator.BusinessLogic.Services
{
    public class CalculationHistoryService: IService
    {
        IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<CalculationHistory> CalculationHistories; // коллекция в базе данных

        public CalculationHistoryService()
        {
            // строка подключения
            string connectionString = "mongodb://localhost:27017/mobilestore";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к бд
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
            // обращаемся к коллекции CalculationHistory
            CalculationHistories = database.GetCollection<CalculationHistory>("CalculationHistories");
        }

        public async Task Create(CalculationHistory model)
        {
            await CalculationHistories.InsertOneAsync(model);
        }

        public async Task Update(CalculationHistory model)
        {
            await CalculationHistories.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(model.Id)), model);
        }

        public async Task Remove(string id)
        {
            await CalculationHistories.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<CalculationHistory> GetById(string id)
        {
            return await CalculationHistories.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CalculationHistory>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<CalculationHistory>();
            var filter = builder.Empty;

            return await CalculationHistories.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<CalculationHistory>> GetByDate(DateTime date)
        {
            var builder = new FilterDefinitionBuilder<CalculationHistory>();
            var filter = builder.Empty;

            if (date != DateTime.MinValue)
            {
                filter = filter & builder.Where(d => d.Date == date);
            }
            return await CalculationHistories.Find(filter).ToListAsync();
        }
    }
}
