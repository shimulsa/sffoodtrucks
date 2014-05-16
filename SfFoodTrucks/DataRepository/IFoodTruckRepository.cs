using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SfFoodTrucks.Models;

namespace SfFoodTrucks.DataRepository
{
    public interface IFoodTruckRepository
    {
        /// <summary>
        /// Get a list of ALL approved food trucks in SF close to a given location
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <param name="limit">Max results to return</param>
        /// <returns>List of ALL approved food trucks in SF close to the given location</returns>
        IEnumerable<FoodTruck> GetApprovedFoodTrucks(double latitude, double longitude, int limit);
    }
}