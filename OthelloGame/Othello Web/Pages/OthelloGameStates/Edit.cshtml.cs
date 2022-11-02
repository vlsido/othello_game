using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGameStates
{
    public class EditModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public EditModel(Othello_Web.Data.ApplicationDbContext context)
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

            var othellogamestate =  await _context.OthelloGamesStates.FirstOrDefaultAsync(m => m.Id == id);
            if (othellogamestate == null)
            {
                return NotFound();
            }
            OthelloGameState = othellogamestate;
           ViewData["OthelloGameId"] = new SelectList(_context.OthelloGames, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OthelloGameState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OthelloGameStateExists(OthelloGameState.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OthelloGameStateExists(int id)
        {
          return (_context.OthelloGamesStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
