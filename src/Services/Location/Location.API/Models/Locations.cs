using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Location.API.Models
{
    public class Locations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ParentCode { get; set; }
        public string LocationCode { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; private set; }

        public int Level { get; set; }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        public Locations SetLocation(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));
            return this;
        }
    }
}
