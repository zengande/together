using System.ComponentModel.DataAnnotations;

namespace Together.Notice.ViewModels
{
    public class MialboxViewModel
    {
        [Required]
        public string[] Tos { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
