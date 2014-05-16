using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SfFoodTrucks;
using SfFoodTrucks.Controllers;
using SfFoodTrucks.DataRepository;

namespace SfFoodTrucks.Tests.Controllers
{
    [TestClass]
    public class FoodTruckRepositoryTest
    {
        [TestMethod]
        public void FoodRepCache()
        {
            var rep = new FoodTruckRepository();        
            double testLatitude1 = 37.7833;
            double testLongitude1 = -122.4167;
            var result = rep.GetApprovedFoodTrucks(testLatitude1, testLongitude1, 10);
            Assert.IsNotNull(result);
            Assert.IsNotNull(rep.allFoodTrucksCache.Get("FoodTrucks"));
        }

        [TestMethod]
        public void FoodRepCalcProximitySameLoc()
        {
            var rep = new FoodTruckRepository();
            // Arrange
            double testLatitude1 = 37.7833;
            double testLongitude1 = -122.4167;
            var dist = rep.CalcProximity(testLatitude1, testLongitude1, testLatitude1, testLongitude1);

            // Assert
            Assert.IsNotNull(dist);
            Assert.IsTrue(dist < 0.0005);
        }

        [TestMethod]
        public void FoodRepCalcProximityZero()
        {
            var rep = new FoodTruckRepository();
            // Arrange
            double testLatitude1 = 37.7833;
            double testLongitude1 = -122.4167;
            var dist = rep.CalcProximity(0, 0, 0, 0);

            // Assert
            Assert.IsNotNull(dist);
            Assert.AreEqual(dist, 0);
        }
    }
}
