using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGameStates
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
        ViewData["OthelloGameId"] = new SelectList(_context.OthelloGames, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public OthelloGameState OthelloGameState { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.OthelloGamesStates == null || OthelloGameState == null)
            {
                return Page();
            }

            _context.OthelloGamesStates.Add(OthelloGameState);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
