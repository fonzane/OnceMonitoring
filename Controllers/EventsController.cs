using System.Text.Json;
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
        public async Task<IActionResult> PostData([FromBody] JsonElement jsonData)
        {
            try
            {
                Console.WriteLine($"Received JSON: {jsonData}");

                List<EventDataModel> dataList = new();

                // Check if jsonData is an object (`{}`)
                if (jsonData.ValueKind == JsonValueKind.Object)
                {
                    Console.WriteLine("Received a single object instead of a list.");
                    var singleItem = JsonSerializer.Deserialize<EventDataModel>(jsonData.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (singleItem != null)
                    {
                        dataList.Add(singleItem);
                    }
                }
                // Check if jsonData is an array (`[...]`)
                else if (jsonData.ValueKind == JsonValueKind.Array)
                {
                    Console.WriteLine("Received a list of objects.");
                    dataList = JsonSerializer.Deserialize<List<EventDataModel>>(jsonData.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EventDataModel>();
                }
                else
                {
                    Console.WriteLine("Invalid JSON format received.");
                    return Ok("Invalid JSON format. Expected an object or an array.");
                }

                if (!dataList.Any())
                {
                    Console.WriteLine("No valid data found. Returning NoContent.");
                    return NoContent();
                }

                // Process and insert data into MongoDB
                var collection = _databaseConfig._collection;
                bool dataSaved = false;

                foreach (var item in dataList)
                {
                    if (DataFilterService.FilterData(item))
                    {
                        dataSaved = true;
                        await collection.InsertOneAsync(item.ToBsonDocument());
                    }
                }

                if (dataSaved)
                {
                    return Ok("Data saved successfully.");
                }
                else
                {
                    return Ok("Couldn't find matching data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, "An internal server error occurred.");
            }

        }
    }
}