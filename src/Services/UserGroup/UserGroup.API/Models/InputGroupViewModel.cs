using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Models
{
    public class InputGroupViewModel
    {
        [Required]
        public string GroupName { get; set; }
        public string Introduction { get; set; }

        public Group ToEntity() => new Group
        {
            GroupName = GroupName,
            Introduction = Introduction
        };
    }
}
