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
        public List<FoodTruck> Index(double latitude, double longitude, int limit = 50)
        {
            DataRepository.IFoodTruckRepository dataRep = new DataRepository.FoodTruckRepository();
            var nearbyTrucks = dataRep.GetApprovedFoodTrucks(latitude, longitude, limit);
            return nearbyTrucks.ToList();
        }
    }
}
