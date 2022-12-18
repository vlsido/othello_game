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
using OthelloGameBrain;

namespace Othello_Web.Pages_OthelloGames
{
    public class CreateModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        [BindProperty]
        public OthelloGame OthelloGame { get; set; } = default!;

        public OthelloGameState GameState { get; set; } = default!;

        public OthelloBrain Brain { get; set; } = default!;

        public BoardSquareState[,] Board { get; set; } = default!;

        public OthelloOption OthelloOption { get; set; } = default!;

        public SelectList OptionSelectList { get; set; } = default!;

        public CreateModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            OptionSelectList = new SelectList(_context.OthelloOptions, "Id", "Name");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            OthelloOption = await _context.OthelloOptions.FirstOrDefaultAsync(o => o.Id == OthelloGame.OthelloOptionId) ?? throw new InvalidOperationException();
            OthelloGame.OthelloOption = OthelloOption;

            _context.OthelloGames.Add(OthelloGame);
            await _context.SaveChangesAsync();

            var game = _context.OthelloGames.FirstOrDefault(g => g.Id == OthelloGame.Id);


            if (game != null) return RedirectToPage("/OthelloGames/Index");
            else return NotFound();
        }
    }
}
