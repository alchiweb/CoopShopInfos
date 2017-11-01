using System.ComponentModel.DataAnnotations;

namespace CoopShopInfos.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(13)]
        public string Barcode { get; set; }
        [Required, StringLength(100)]
        public string ProductName { get; set; }
    }
}
