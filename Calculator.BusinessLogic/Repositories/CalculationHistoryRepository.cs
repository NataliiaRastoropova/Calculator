using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BusinessLogic.Models;
using Calculator.BusinessLogic.Repositories.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Calculator.BusinessLogic.Repositories
{
    public class CalculationHistoryRepository: BaseRepository
    {
        IGridFSBucket m_gridFS;  
        IMongoCollection<CalculationHistoryModel> m_collection; 

        public CalculationHistoryRepository():base()
        {
            m_gridFS = new GridFSBucket(m_mongoDatabase);
            m_collection = m_mongoDatabase.GetCollection<CalculationHistoryModel>("CalculationHistories");
        }

        public async Task Create(CalculationHistoryModel model)
        {
            await m_collection.InsertOneAsync(model);
        }

        public async Task Remove(string id)
        {
            await m_collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<CalculationHistoryModel> GetById(string id)
        {
            return await m_collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CalculationHistoryModel>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<CalculationHistoryModel>();
            var filter = builder.Empty;

            return await m_collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<CalculationHistoryModel>> GetByDate(DateTime date)
        {
            var builder = new FilterDefinitionBuilder<CalculationHistoryModel>();
            var filter = builder.Empty;

            if (date != DateTime.MinValue)
            {
                filter = filter & builder.Where(d => d.CalculationDate == date);
            }
            return await m_collection.Find(filter).ToListAsync();
        }
    }
}
