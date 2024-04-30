using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.Models;
using Riode.ViewModels;

namespace Riode.Controllers
{
    public class ShopController : Controller 
    {
        private readonly RiodeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
		public ShopController(RiodeDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Where(p => p.IsStock)
                .Include(p => p.Category)
                .ToListAsync();
            var productImages = await _context.ProductImages.ToListAsync();

            var productImageViewModel = new ProductImageViewModel
            {
                Products = products,
                ProductImages = productImages
            };

            return View(productImageViewModel);
        }
        public async Task<IActionResult> LoadMore(int skip)
        {
            var products = await _context.Products.Where(p => !p.IsDeleted).Where(p => p.IsStock).Skip(skip).Take(3).Include(p => p.Category).ToListAsync();
            var productImages = await _context.ProductImages.ToListAsync();

            var productImageViewModel = new ProductImageViewModel
            {
                Products = products,
                ProductImages = productImages
            };

            return PartialView("_ProductPartial", productImageViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Search()
        {
            string searchItem = Request.Query["search"].ToString();
            if (!string.IsNullOrEmpty(searchItem))
            {
                searchItem = searchItem.ToUpper();

                var foundProducts = await _context.Products
                    .Where(p => !p.IsDeleted)
                    .Where(p => p.Name.ToUpper().Contains(searchItem))
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .ToListAsync();

                var productImages = await _context.ProductImages.ToListAsync();

                var productImageViewModel = new ProductImageViewModel
                {
                    Products = foundProducts,
                    ProductImages = productImages
                };

                return View(productImageViewModel);
            }
            else
            {
                return View(null);
            }
        }
		[Authorize]
		public async Task<IActionResult> AddProductToBasket(int productId)
		{
			var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
			if (products == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId == user.Id);

			if (basketItem == null)
			{
				BasketItem newBasketItem = new()
				{
					ProductId = productId,
					AppUserId = user.Id,
					Count = 1,
                    CreatedDate = DateTime.UtcNow
				};

				await _context.BasketItems.AddAsync(newBasketItem);
			}
			else
			{
				basketItem.Count++;
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Index");

		}
	}
}
