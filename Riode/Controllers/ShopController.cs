using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.ViewModels;

namespace Riode.Controllers
{
    public class ShopController : Controller 
    {
        private readonly RiodeDbContext _context;
        public ShopController(RiodeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Where(p => !p.IsDeleted).Where(p => p.IsStock).Include(p => p.Category).ToListAsync();
            var productImages = await _context.ProductImages.ToListAsync();

            var productImageViewModel = new ProductImageViewModel
            {
                Products = products,
                ProductImages = productImages
            };

            return View(productImageViewModel);
        }
    }
}
