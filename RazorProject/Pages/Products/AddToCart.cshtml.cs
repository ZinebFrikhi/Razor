using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Models;

namespace RazorProject.Pages.Products
{
    public class AddToCartModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;
        private readonly GlobalListService _globalListService;

  
        public AddToCartModel(RazorProject.Data.ApplicationDbContext context, GlobalListService globalListService)
        {
            _context = context;
            _globalListService = globalListService;
        }

        [BindProperty]
        public Models.Product Product { get; set; } = default!;
        public List<Product> ShoppingCart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            var globalList = _globalListService.GetGlobalList();
            // Add items to the list
            _globalListService.AddToGlobalList(product);
            TempData["SuccessMessage"] = "Item deleted from the cart.";
            return RedirectToPage("./Index");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TempData["SuccessMessage"] = "Item deleted from the cart.";


            return RedirectToPage("./Index");
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
    
