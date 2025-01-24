using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OnceMonitoring.Config;
using OnceMonitoring.Models;
using WebServerExample.Services;

namespace WebServerExample.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello world");
        }
    }
    [ApiController]
    [Route("events")]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly DatabaseConfig _databaseConfig;

        public EventController(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] List<EventDataModel> data)
        {
            bool dataSaved = false;
            var collection = _databaseConfig._collection;

            if (data == null || !data.Any()) {
                return BadRequest("Invalid data.");
            }

            foreach (EventDataModel item in data) {
                if (DataFilterService.FilterData(item))
                {
                    dataSaved = true;
                    // Get the MongoDB collection

                    // Convert data to BSON and insert into MongoDB
                    var bsonData = item.ToBsonDocument();
                    await collection.InsertOneAsync(bsonData);
                }
            }
            if (dataSaved)
            {
                return Ok("Data saved successfully.");
            } else
            {
                return Ok("Couldn't find matching data.");
            }

        }
    }
}