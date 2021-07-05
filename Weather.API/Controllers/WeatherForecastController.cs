using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration) {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("{id}")]
        public string Get(int id) {
            return "suckmydick" + id + _configuration["ConnectionStrings:Host"];
        }

        [HttpGet("story", Name = "GetStory")]
        public string GetStory() {
            var res = "";

            using (StreamReader sr = new StreamReader("Db/Db.txt"))
                res = sr.ReadToEnd();

            return res;
        }
        [HttpPost("add_story", Name = "AddStory")]
        public bool AddStory([FromBody] Story story) {

            using (StreamWriter sw = new StreamWriter("Db/Db.txt", append: true)) {

                sw.WriteLine(story.s);
            }
            return true;
        }
    }
    public record Story(string s);
}
