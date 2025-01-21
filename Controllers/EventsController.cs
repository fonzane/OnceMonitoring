using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebServerExample.Config;
using WebServerExample.Models;
using WebServerExample.Services;

namespace WebServerExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DatabaseConfig _databaseConfig;

        public DataController(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] EventDataModel data)
        {
            if (DataFilterService.FilterData(data))
            {
                // Get the MongoDB collection
                var collection = _databaseConfig._collection;

                // Convert data to BSON and insert into MongoDB
                var bsonData = data.ToBsonDocument();
                await collection.InsertOneAsync(bsonData);

                return Ok("Data saved successfully.");
            }

            return BadRequest("Data does not meet the filter criteria.");
        }
    }
}