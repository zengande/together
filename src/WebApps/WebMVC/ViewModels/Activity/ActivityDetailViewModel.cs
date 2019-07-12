using System;
using System.Collections.Generic;
using WebMVC.Infrastructure.Enums;

namespace WebMVC.ViewModels.Activity
{
    public class ActivityDetailViewModel
    {
        public ActivityDetailViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }

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
        public ActivityStatus Status { get; set; }
        public int NumberOfParticipants { get; set; }
        public bool IsJoined { get; set; }
        public AddressVisibleRules AddressVisibleRule { get; set; }

        public List<ParticipantViewModel> Participants { get; set; }
    }

    public class ParticipantViewModel
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public DateTime JoinTime { get; set; }
        public string Avatar { get; set; }
        public int Sex { get; set; }
        public bool IsOwner { get; set; }
    }
}
