using Microsoft.AspNetCore.Mvc;
using PVPredictor.WebApi.Models;
using PVPredictor.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVPredictor.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController :ControllerBase
    {
        private readonly CitiesService _citiesService;

        public CitiesController(CitiesService citiesService) => _citiesService = citiesService;

        [HttpGet]
        public async Task<List<City>> Get() =>
            await _citiesService.GetAsync();
    }
}
