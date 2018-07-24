using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Activities
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }

        [Required]
        public string Title { get; set; }

        public bool HasFee { get; set; }
    }
}
