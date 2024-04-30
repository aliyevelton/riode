using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.Models;
using Riode.ViewModels;
namespace Riode.ViewComponents
{
    public class BasketViewComponent
    {
        private readonly RiodeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BasketViewComponent(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItem = _context.BasketItems
                .Where(b => b.AppUserId == user.Id)
                .ToList();


            return View(basketItem);
        }
    }
}
