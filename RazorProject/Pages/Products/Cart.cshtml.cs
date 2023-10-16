using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Models;
using System.Xml.Linq;

namespace RazorProject.Pages.Products
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GlobalListService _globalListService;


        public CartModel(IHttpContextAccessor httpContextAccessor, GlobalListService globalListService)
    {
        _httpContextAccessor = httpContextAccessor;
            _globalListService = globalListService;

        }
        public IList<Product> Products { get; set; } = default!;
        public IActionResult OnGet()
    {

            Models.Product prod = new Models.Product();
            var globalList = _globalListService.GetGlobalList();
            // Add items to the list
            Products=globalList;

        return Page();
    }
        [BindProperty]
        public int Id { get; set; }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _globalListService.DeleteFromGlobalList(id);
            TempData["SuccessMessage"] = "Item deleted from the cart.";

            return RedirectToPage("./Cart");
        }
      
        public decimal TotalPrice
        {
            get
            {
                return _globalListService.GetGlobalList().Sum(item => item.Price);
            }
        }
    }
}