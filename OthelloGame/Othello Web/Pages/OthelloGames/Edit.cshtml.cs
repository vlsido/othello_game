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

namespace Othello_Web.Pages_OthelloGames
{
    public class EditModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public EditModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OthelloGame OthelloGame { get; set; } = default!;

        [BindProperty]
        public OthelloOption OthelloOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Othello games
            if (id == null || _context.OthelloGames == null)
            {
                return NotFound();
            }


            var othelloGame =  await _context.OthelloGames.FirstOrDefaultAsync(m => m.Id == id);
            var othelloOption = await _context.OthelloOptions.FirstOrDefaultAsync(m => m.Id == othelloGame!.OthelloOptionId);

            if (othelloGame == null || othelloOption == null)
            {
                return NotFound();
            }
            OthelloGame = othelloGame;
            OthelloOption = othelloOption;
           ViewData["Name"] = new SelectList(_context.OthelloOptions, "Name", "Name");
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

            _context.Attach(OthelloGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OthelloGameExists(OthelloGame.Id))
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

        private bool OthelloGameExists(int id)
        {
          return (_context.OthelloGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool OthelloOptionExists(int id)
        {
            return (_context.OthelloOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
