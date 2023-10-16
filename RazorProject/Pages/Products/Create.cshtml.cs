using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;
        public List<SelectListItem> Options { get; set; }
 
        public CreateModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Options = _context.Categories.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();

            if (Product != null && Product.Category != null)
            {
                foreach (var option in Options)
                {
                    if (int.Parse(option.Value) == Product.Category.Id)
                    {
                        option.Selected = true;
                    }
                }
            }

            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }

            // Ensure that the selected category is loaded from the database
            Product.Category = _context.Categories.Find(Product.Category.Id);

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
