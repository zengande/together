using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Address
        : ValueObject
    {
        public string DetailAddress { get; set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public string Province { get; private set; }
        public string Location { get; private set; }
        public Address(string province,
            string city,
            string county,
            string detailAddress,
            string location)
        {
            Province = province;
            City = city;
            County = county;
            DetailAddress = detailAddress;
            Location = location;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DetailAddress;
            yield return City;
            yield return County;
            yield return Province;
            yield return Location;
        }
    }
}
