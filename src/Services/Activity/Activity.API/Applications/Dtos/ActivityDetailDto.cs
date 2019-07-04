using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Together.Activity.API.Applications.Dtos
{
    public class ActivityDetailDto
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public DateTime ActivityEndTime { get; set; }
        public DateTime EndRegisterTime { get; set; }
        public string DetailAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int? LimitsNum { get; set; }

        [JsonProperty("status")]
        public int ActivityStatusId { get; set; }
        public int NumberOfParticipants { get; set; }
        public bool IsJoined { get; set; }

        public IEnumerable<ParticipantDto> Participants { get; set; }

        public ActivityDetailDto()
        {
            Participants = new List<ParticipantDto>();
        }
    }
}
