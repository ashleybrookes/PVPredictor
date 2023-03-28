using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PVPredictor.WebApi.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("City")]
        public string CityName { get; set; } = null!;
        public string Country { get; set; } = null!;

        [BsonElement("LATITUDE")]
        public decimal Latitude { get; set; }

        [BsonElement("LONGITUDE")]
        public decimal Longitude { get; set; }

    }
}
