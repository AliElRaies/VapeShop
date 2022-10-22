using System.ComponentModel.DataAnnotations;

namespace VapeShop.Models
{
    public class VapeItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "upload image please!")]
        public string VapeImg { get; set; }

        [Required(ErrorMessage = "Company Name")]
        public string Maker { get; set; }
        
        [Required(ErrorMessage = "Enter Model Name//Number")]
        public string Model { get; set; }

        public VapeItem()
        {

        }
    }
}
