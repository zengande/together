using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Elasticsearch.Models
{
    [ElasticsearchType(RelationName = "activity")]
    public class ActivityIndex
    {
        [Number(Index = true)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime EndRegisterTime { get; set; }
        public DateTime CreateTimeUtc { get; set; }
        public DateTime ActivityStartTimeUtc { get; set; }
        public DateTime ActivityEndTimeUtc { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string DetailAddress { get; set; }
        public GeoLocation Location { get; set; }
        public int CatalogId { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }

        public ActivityIndex(int id, string title, string content, DateTime createTimeUtc, DateTime endRegisterTime, DateTime activityStartTimeUtc, DateTime activityEndTimeUtc, string city, string county, string detailAddress, double latitude, double longitude, int catalogId, string creatorId, string creatorName)
        {
            Id = id;
            Title = title;
            Content = content;
            CreateTimeUtc = createTimeUtc;
            EndRegisterTime = endRegisterTime;
            ActivityStartTimeUtc = activityStartTimeUtc;
            ActivityEndTimeUtc = activityEndTimeUtc;
            City = city;
            County = county;
            DetailAddress = detailAddress;
            Location = new GeoLocation(latitude, longitude);
            CatalogId = catalogId;
            CreatorId = creatorId;
            CreatorName = creatorName;
        }
    }
}
