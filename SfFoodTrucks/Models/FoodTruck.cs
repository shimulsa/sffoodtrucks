using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SfFoodTrucks.Models
{
    /// <summary>
    /// Object model for JSON response
    /// </summary>
    [DataContract]
    public class FoodTruck
    {
        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name="status")]
        public string Status { get; set; }

        /// <summary>
        /// Applicant name
        /// </summary>
        [DataMember(Name = "applicant")]
        public string Applicant { get; set; }

        /// <summary>
        /// Schedule
        /// </summary>
        [DataMember(Name = "schedule")]
        public string Schedule { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Lot
        /// </summary>
        [DataMember(Name = "lot")]
        public string Lot { get; set; }

        /// <summary>
        /// Block 
        /// </summary>
        [DataMember(Name = "block")]
        public string Block { get; set; }

        /// <summary>
        /// Food Items
        /// </summary>
        [DataMember(Name = "fooditems")]
        public string FoodItems { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Distance from Searched Location
        /// </summary>
        public double DistFromSearchedLoc { get; set; }
    }
}