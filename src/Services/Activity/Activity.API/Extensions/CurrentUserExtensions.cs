using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Applications.Dtos;
using Together.Activity.API.Models;

namespace Together.Activity.API.Extensions
{
    public static class CurrentUserExtensions
    {

        public static ParticipantDto ToParticipantDto(this CurrentUser user) => new ParticipantDto
        {
            Avatar = user.Avatar,
            Nickname = user.Nickname,
            UserId = user.UserId
        };

        public static IEnumerable<ParticipantDto> ToParticipantsDto(this IEnumerable<CurrentUser> users)
        {
            foreach(var user in users)
            {
                yield return user.ToParticipantDto();
            }
        }
    }
}
