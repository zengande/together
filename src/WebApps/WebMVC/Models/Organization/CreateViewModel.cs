using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Organization
{
    public class CreateViewModel
    {
        [Required]
        public string Location { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="最多50个字符")]
        public string GroupName { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "最多200个字符")]
        public string Description { get; set; }
    }
}
