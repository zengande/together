using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Address
        : ValueObject
    {
        public string DetailAddress { get; set; }
        public string City { get; private set; }
        public string County { get; private set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        public Address(string city,
            string county,
            string detailAddress,
            double longitude, double latitude)
        {
            City = city;
            County = county;
            DetailAddress = detailAddress;
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DetailAddress;
            yield return County;
            yield return City;
        }
    }
}
