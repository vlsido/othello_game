using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloOptions
{
    public class CreateModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public CreateModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OthelloOption OthelloOption { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.OthelloOptions == null || OthelloOption == null)
            {
                return Page();
            }

            _context.OthelloOptions.Add(OthelloOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
