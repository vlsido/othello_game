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
    public class EditModel : PageModel
    {
        

        [BindProperty]
        public OthelloGame OthelloGame { get; set; } = default!;

        [BindProperty]
        public OthelloOption OthelloOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            // Othello games
            if (id == null || othelloDb.OthelloGames == null)
            {
                return NotFound();
            }


            var othelloGame =  await othelloDb.OthelloGames.FirstOrDefaultAsync(m => m.Id == id);
            var othelloOption = await othelloDb.OthelloOptions.FirstOrDefaultAsync(m => m.Id == othelloGame!.OthelloOptionId);

            if (othelloGame == null || othelloOption == null)
            {
                return NotFound();
            }
            OthelloGame = othelloGame;
            OthelloOption = othelloOption;
           ViewData["Name"] = new SelectList(othelloDb.OthelloOptions, "Name", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            othelloDb.Attach(OthelloGame).State = EntityState.Modified;

            try
            {
                await othelloDb.SaveChangesAsync();
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
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            return (othelloDb.OthelloGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool OthelloOptionExists(int id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            return (othelloDb.OthelloOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
