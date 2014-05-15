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
        public List<FoodTruck> Index()
        {
            List<FoodTruck> nearbyTrucks = new List<FoodTruck>();
            var ft1 = new FoodTruck()
            {
                XCoord = 37.75833,
                YCoord = -122.45628,
                Applicant = "SF Momos",
                FoodItems = "Momos, Burritos, Coke, Lemonade"
            };

            var ft2 = new FoodTruck()
            {
                XCoord = 37.75933,
                YCoord = -122.45828,
                Applicant = "Curry up now",
                FoodItems = "Deconstructed Burrito, Kathi roll"
            };

            nearbyTrucks.Add(ft1);
            nearbyTrucks.Add(ft2);
            return nearbyTrucks;
        }
    }
}
