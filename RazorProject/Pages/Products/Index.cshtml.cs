using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;
        public List<SelectListItem> Options { get; set; }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public IndexModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }


        public IActionResult OnGet()
        {

                Options = _context.Categories.Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    }).ToList();

            var productsQuery = _context.Products.AsQueryable();

       /*     if (!string.IsNullOrEmpty(SearchString))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            }*/
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Filter by product name using a case-insensitive search
                var searchLower = SearchString.ToLower(); // Convert search string to lowercase
                productsQuery = productsQuery
                    .Where(p => p.Name.ToLower().Contains(searchLower));
            }

            if (CategoryId != 0)
            {
                productsQuery = productsQuery
                    .Where(p => p.Category.Id == CategoryId);
            }

            var filteredProducts = productsQuery.ToList();
            Products = filteredProducts;
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products.ToListAsync();
            }
            var products = from m in _context.Products
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }
            Products = await products.ToListAsync();
            return RedirectToPage("./Index");

        }

    }
}
