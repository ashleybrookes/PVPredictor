using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Collections;

namespace SolarCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolarCalculatorController : ControllerBase
    {
        private readonly ILogger<SolarCalculatorController> _logger;

        private static readonly Hashtable cityLongitudeCoordinates = new Hashtable
        {
            { "berlin" , 13.4 },
            { "gothenburg" , 11.97 },
            { "london" ,0.12 }
        };
            private static readonly Hashtable cityLatitudeCoordinates = new Hashtable
        {
            { "berlin" , 52.5 },
            { "gothenburg" , 57.71 },
            { "london" , 51.5 }
        };

        public SolarCalculatorController(ILogger<SolarCalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LocationSolarData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get_IActionResult(string city)
        {
            //todo make this return the data
            //look up example where we hand in the city maybe
            if (city == null)
            {
                return NotFound();
            }
            if (city.Length == 0)
            {
                //todo return proper message in the bad request
                BadRequest();
            }
            city = city.ToLower();

            //todo:load city data from json file
            List<LocationSolarData> SolarData = new List<LocationSolarData>();

            if (cityLatitudeCoordinates[city.ToLower()] == null) {
                return NotFound();
            }
            
            if (cityLongitudeCoordinates[city.ToLower()] == null) {
                return NotFound();
            }
            double citylat = (double)cityLatitudeCoordinates[city];
            double citylng = (double)cityLongitudeCoordinates[city];

            //lets make some assumptions about the month
            //get the month of the year
            //get the formula
            //Max Solar Power at sea level at equator is 1000 W/m2 Watts per meter squared 361 Watts is lost through atmospheric absorption and reflection

                //need to add 180 onto longitude to convert to 0-360 value

            //https://en.wikipedia.org/wiki/Solar_irradiance#Derivation

            SolarData.Add(new SolarCalculator.LocationSolarData { dt = DateTime.Now, latitude = (double)cityLatitudeCoordinates[city], longitude = (double)cityLongitudeCoordinates[city] , solarMaximum = 1, solarPercentage = 1, solarPowerWatts = 1 });
            
            
            
            //TODO put in code to change the values here 
            return Ok(SolarData);
        }
    }
}
