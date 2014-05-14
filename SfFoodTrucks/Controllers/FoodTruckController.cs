using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SfFoodTrucks.Models;

namespace SfFoodTrucks.Controllers
{
    public class FoodTruckController : ApiController
    {
        [HttpGet]
        public List<Location> Index()
        {
            List<Location> nearbyItems = new List<Location>();
            var loc = new Location();
            loc.XCoord = 37.75833;
            loc.YCoord = -122.45628;
            nearbyItems.Add(loc);
            return nearbyItems;
        }
    }
}
