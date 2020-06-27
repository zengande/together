using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Location.Domain.Locations
{
    public class LocationArea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public int Code { get; private set; }
        public int? ParentCode { get; private set; }
        public string Name { get; private set; }
        public string MergeName { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; private set; }

        public void SetLocation(double lon, double lat)
        {
            Latitude = lat;
            Longitude = lon;
            Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(lon, lat));
        }

        private LocationArea() { }

        public LocationArea(int code, string name, string mergeName, int? parentCode = 0)
        {
            Code = code;
            Name = name;
            MergeName = mergeName;
            ParentCode = parentCode;
        }
    }
}
