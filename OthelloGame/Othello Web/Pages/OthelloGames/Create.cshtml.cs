using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OthelloGameBrain;

namespace Othello_Web.Pages_OthelloGames
{
    public class CreateModel : PageModel
    {

        

        [BindProperty]
        public OthelloGame OthelloGame { get; set; } = default!;

        public OthelloGameState GameState { get; set; } = default!;

        public OthelloBrain Brain { get; set; } = default!;

        public BoardSquareState[,] Board { get; set; } = default!;

        public OthelloOption OthelloOption { get; set; } = default!;

        public SelectList OptionSelectList { get; set; } = default!;

        

        public IActionResult OnGet()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);

            OptionSelectList = new SelectList(othelloDb.OthelloOptions, "Id", "Name");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);

            OthelloOption = await othelloDb.OthelloOptions.FirstOrDefaultAsync(o => o.Id == OthelloGame.OthelloOptionId) ?? throw new InvalidOperationException();
            OthelloGame.OthelloOption = OthelloOption;

            othelloDb.OthelloGames.Add(OthelloGame);
            await othelloDb.SaveChangesAsync();

            var game = othelloDb.OthelloGames.FirstOrDefault(g => g.Id == OthelloGame.Id);


            if (game != null) return RedirectToPage("/OthelloGames/Index");
            else return NotFound();
        }
    }
}
