using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YumBlazor.Data
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        
        public OrderHeader OrderHeader { get; set; }
        public int ProductIt { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ProductName{ get; set; }
    }
}
