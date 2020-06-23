using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Dtos
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string DetailAddress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int? LimitsNum { get; set; }
        public DateTime EndRegisterTime { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public DateTime ActivityEndTime { get; set; }
        public int ActivityStatusId { get; set; }
        public bool ShowAddress { get; set; }
        public bool IsCreator { get; set; }
        public bool IsJoined { get; set; }
        public bool IsCollected { get; set; }
        public int? NumOfP { get; set; }
    }
}
