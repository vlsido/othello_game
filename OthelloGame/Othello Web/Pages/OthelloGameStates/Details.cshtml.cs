using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Othello_Web.Pages_OthelloGameStates
{
    public class DetailsModel : PageModel
    {


      public OthelloGameState OthelloGameState { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (id == null || othelloDb.OthelloGamesStates == null)
            {
                return NotFound();
            }

            var othellogamestate = await othelloDb.OthelloGamesStates.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
