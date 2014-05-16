using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FoodTruckControllerTest
    {
        [TestMethod]
        public void GetApiBasic()
        {
            // Arrange
            double testLatitude = 37.3475;
            double testLongitude = -122.7865;
            int testLimit = 10;
            FoodTruckController controller = new FoodTruckController();

            // Act
            var result = controller.Index(testLatitude, testLongitude, testLimit);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testLimit, result.Count);

            result = controller.Index(testLatitude, testLongitude, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            // Check default "limit" behavior, i.e., 50
            result = controller.Index(testLatitude, testLongitude);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.Count);
        }

        [TestMethod]
        public void GetApiInvalid()
        {
            // Arrange
            FoodTruckController controller = new FoodTruckController();

            // Act
            var result = controller.Index(0, 0, -1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetApiApprovedTrucks()
        {
            var rep = new FoodTruckRepository();
            var allTrucks = rep.GetAllFoodTrucks();

            // Arrange
            double testLatitude = 37.7833;
            double testLongitude = -122.4167;
            FoodTruckController controller = new FoodTruckController();

            // Act - get ALL approved trucks with a valid location
            var allApproved = controller.Index(testLatitude, testLongitude, allTrucks.Count());
            var numApprovedExpected = allTrucks.Where(x => x.Status.Equals("APPROVED") && x.Latitude != 0.0 && x.Longitude != 0.0).Count();

            // Assert
            Assert.IsNotNull(allApproved);
            Assert.AreEqual(numApprovedExpected, allApproved.Count);
        }
    }
}
