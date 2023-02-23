using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Feature.API.Logger;
using Feature.BusinessModel.ViewModel;
using Feature.Services.Abstract;
using Feature.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Microsoft.Graph;

namespace Feature.API.Controllers
{
    
        [ApiController]
        [Route("[controller]")]
    public class WeatherForecastSerilogController : ControllerBase
        {
            private static readonly string[] Summaries = new[]
            {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
        private readonly ILogger<WeatherForecastSerilogController> _logger;
        private readonly ILogger<WeatherForecastSerilogController> _seriLogger;
        private readonly ILoggerExtention _nlogLogger;
        private readonly IUserService _userService;
        public WeatherForecastSerilogController(ILogger<WeatherForecastSerilogController> logger, ILogger<WeatherForecastSerilogController> seriLogger, ILoggerExtention nlogLogger, IUserService userService)
        {
            _seriLogger = seriLogger;
            _nlogLogger = nlogLogger;
            _logger = logger;
            _userService = userService;
        }
        // GET api/values    
        /// <summary>    
        /// WeatherForecastController Api Get method    
        /// </summary>    
        /// <returns></returns>   
        
        [HttpGet(Name = "GetWeatherForecast")]
        
         public IEnumerable<WeatherForecast> Get()
        {
            _seriLogger.LogDebug("Inside GetWeatherForecast endpoint");
            _nlogLogger.LogInformation("Initiate log");
            _logger.LogInformation("Initiating Weather Logs");
            var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
            _logger.LogDebug($"The response for the get weather forecast is {JsonConvert.SerializeObject(response)}");
            return response;
        }

       
    }
}
