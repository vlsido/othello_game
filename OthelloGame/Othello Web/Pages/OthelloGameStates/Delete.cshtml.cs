using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGameStates
{
    public class DeleteModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public DeleteModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OthelloGameState OthelloGameState { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OthelloGamesStates == null)
            {
                return NotFound();
            }

            var othellogamestate = await _context.OthelloGamesStates.FirstOrDefaultAsync(m => m.Id == id);

            if (othellogamestate == null)
            {
                return NotFound();
            }
            else 
            {
                OthelloGameState = othellogamestate;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OthelloGamesStates == null)
            {
                return NotFound();
            }
            var othellogamestate = await _context.OthelloGamesStates.FindAsync(id);

            if (othellogamestate != null)
            {
                OthelloGameState = othellogamestate;
                _context.OthelloGamesStates.Remove(OthelloGameState);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
