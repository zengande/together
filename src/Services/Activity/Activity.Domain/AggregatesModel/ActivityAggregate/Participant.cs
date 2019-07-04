using System;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public class Participant
        : Entity
    {
        public string UserId { get; private set; }

        public string Nickname { get; private set; }

        public string Avatar { get; private set; }

        public int ActivityId { get; private set; }

        public int Sex { get; private set; }

        public DateTime JoinTime { get; private set; }

        public bool IsOwner { get; private set; }

        public Participant(string userId, string nickname, string avatar, int sex, bool isOwner = false)
        {
            UserId = userId;
            Nickname = nickname;
            Avatar = avatar;
            Sex = sex;
            IsOwner = isOwner;

            JoinTime = DateTimeOffset.Now.DateTime;
        }
    }
}
