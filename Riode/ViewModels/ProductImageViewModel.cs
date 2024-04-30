using Riode.Models;
using System.Drawing;

namespace Riode.ViewModels
{
    public class ProductImageViewModel
    {
        public List<Product> Products { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public string SearchItem { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}
