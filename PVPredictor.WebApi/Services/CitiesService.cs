using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PVPredictor.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVPredictor.WebApi.Services
{
    public class CitiesService
    {
        private readonly IMongoCollection<City> _cityCollection;

        public CitiesService( IOptions<PVPredictorDatabaseSettings> pvPredictorDatabaseSettings)
        {
            var mongoClient = new MongoClient(pvPredictorDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(pvPredictorDatabaseSettings.Value.DatabaseName);

            _cityCollection = mongoDatabase.GetCollection<City>(pvPredictorDatabaseSettings.Value.CitiesCollectionName);
        }

        public async Task<List<City>> GetAsync() => await _cityCollection.Find(_ => true).ToListAsync();

       
    }
}
