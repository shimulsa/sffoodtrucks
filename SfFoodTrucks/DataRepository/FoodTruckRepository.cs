using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Web;
using SfFoodTrucks.Models;

namespace SfFoodTrucks.DataRepository
{
    public class FoodTruckRepository : IFoodTruckRepository
    {
        //Get the default MemoryCache to cache SF foodd truck metadata in memory
        public ObjectCache allFoodTrucksCache = MemoryCache.Default;
        public CacheItemPolicy policy = new CacheItemPolicy();
        private const string dataFoodTrucks = @"http://data.sfgov.org/resource/rqzj-sfat.json";
        private const string dataApprovedFoodTrucks = @"http://data.sfgov.org/resource/rqzj-sfat.json?status=approved";

        /// <summary>
        /// Get a list of ALL approved food trucks in SF close to a given location
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <param name="limit">Max results to return</param>
        /// <returns>List of ALL approved food trucks in SF close to the given location</returns>
        public IEnumerable<FoodTruck> GetApprovedFoodTrucks(double latitude, double longitude, int limit)
        {
            IEnumerable<FoodTruck> currCache = (IEnumerable<FoodTruck>)allFoodTrucksCache.Get("FoodTrucks");
            if (currCache == null)
            {
                // Add the data to the cache
                var foodTrucks = RestClient.GetResponse<IEnumerable<FoodTruck>>(new Uri(dataApprovedFoodTrucks));
                foodTrucks = foodTrucks.Where(x => x.Latitude != 0.0 && x.Longitude != 0.0);
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(8.0);
                allFoodTrucksCache.Add("FoodTrucks", foodTrucks, policy);
            }

            currCache = (IEnumerable<FoodTruck>)allFoodTrucksCache.Get("FoodTrucks");
            currCache.ToList().ForEach(x => x.DistFromSearchedLoc = CalcProximity(latitude, longitude, x.Latitude, x.Longitude));
            return currCache.OrderBy(x => x.DistFromSearchedLoc).Take(limit);
        }

        /// <summary>
        /// Get all food trucks - for testing needs, hence bypassing cache
        /// </summary>
        /// <returns>List of all food trucks</returns>
        public IEnumerable<FoodTruck> GetAllFoodTrucks()
        {
            var foodTrucks = RestClient.GetResponse<IEnumerable<FoodTruck>>(new Uri(dataFoodTrucks));
            return foodTrucks;
        }

        public double CalcProximity(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
                          Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                          Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}