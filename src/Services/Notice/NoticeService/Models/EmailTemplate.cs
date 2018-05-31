using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.Models
{
    public class EmailTemplate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        [Required]
        public string Template { get; set; }

        [StringLength(200)]
        public string KeyWord { get; set; }
    }
}
