using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OthelloGameBrain;

namespace Othello_Web.Pages_OthelloGames
{
    public class DeleteModel : PageModel
    {
        

        [BindProperty]
      public OthelloGame OthelloGame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);

            if (id == null || othelloDb.OthelloGames == null)
            {
                return NotFound();
            }

            var othellogame = await othelloDb.OthelloGames.FirstOrDefaultAsync(m => m.Id == id);

            if (othellogame == null)
            {
                return NotFound();
            }
            else 
            {
                OthelloGame = othellogame;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (id == null || othelloDb.OthelloGames == null)
            {
                return NotFound();
            }
            var othellogame = await othelloDb.OthelloGames.FindAsync(id);

            if (othellogame != null)
            {
                OthelloGame = othellogame;
                othelloDb.OthelloGames.Remove(OthelloGame);
                await othelloDb.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
