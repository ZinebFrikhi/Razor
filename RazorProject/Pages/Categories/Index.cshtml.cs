﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
        }
    }
}
