using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGames
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
        ViewData["OthelloOptionId"] = new SelectList(_context.OthelloOptions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public OthelloGame OthelloGame { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.OthelloGames == null || OthelloGame == null)
            {
                return Page();
            }

            _context.OthelloGames.Add(OthelloGame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
