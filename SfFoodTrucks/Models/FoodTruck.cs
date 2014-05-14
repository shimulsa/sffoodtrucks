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
        /// Location ID
        /// </summary>
        [DataMember(Name = "name")]
        public int LocationId { get; set; }

        /// <summary>
        /// Applicant name
        /// </summary>
        public string Applicant { get; set; }

        /// <summary>
        /// Facility Type
        /// </summary>
        public string FacilityType { get; set; }

        /// <summary>
        /// Location Description
        /// </summary>
        public string LocDesc { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Block Lot
        /// </summary>
        public int BlockLot { get; set; }

        /// <summary>
        /// Lot
        /// </summary>
        public int Lot { get; set; }

        /// <summary>
        /// Block 
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Food Items
        /// </summary>
        public string FoodItems { get; set; }

        /// <summary>
        /// X coordinate
        /// </summary>
        public double XCoord { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double YCoord { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}