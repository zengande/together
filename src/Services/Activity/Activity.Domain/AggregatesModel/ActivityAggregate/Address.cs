using System.Collections.Generic;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Address
        : ValueObject
    {
        public string DetailAddress { get; set; }
        public string City { get; private set; }
        public string Province { get; private set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        public Address(string province,
            string city,
            string detailAddress,
            double longitude, double latitude)
        {
            Province = province;
            City = city;
            DetailAddress = detailAddress;
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DetailAddress;
            yield return City;
            yield return Province;
        }
    }
}
