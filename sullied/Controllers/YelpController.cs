using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sullied_services.Models;
using sullied_services.Models.Filters;
using sullied_services.Services;

namespace sullied.Controllers
{
    [Route("v1/yelp-search")]
    [ApiController]
    public class YelpController : ControllerBase
    {
        private Yelp.Api.Client _client;
        public YelpController()
        {
            _client = new Yelp.Api.Client("fBC3_rQ99_-dUBF_GTCxLX3VMBdMAqKvD4CRq5wNIdhrfpyHyeqfGiSUUBpDxNRBuXBAam2IHYcVwbGAl0ebpu_quYPdCKlzQeWhnSfM6qzqZMVzWZUFxfVqd5urW3Yx");
        }

        // GET api/values
        [HttpGet]
        public async Task<Yelp.Api.Models.SearchResponse> Get([FromQuery] YelpFilters filters)
        {
            var request = new Yelp.Api.Models.SearchRequest();
            request.Term = filters.Term;
            request.Price = filters.Price;
            request.OpenAt = filters.OpenAt;
            request.Location = filters.Location;
            request.Radius = filters.Radius;
            return await _client.SearchBusinessesAllAsync(request);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Yelp.Api.Models.BusinessResponse> Get(string id)
        {
            return await _client.GetBusinessAsync(id);
        }
    }
}
