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

namespace Othello_Web.Pages_OthelloOptions
{
    public class EditModel : PageModel
    {

        [BindProperty]
        public OthelloOption OthelloOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (id == null || othelloDb.OthelloOptions == null)
            {
                return NotFound();
            }

            var othellooption =  await othelloDb.OthelloOptions.FirstOrDefaultAsync(m => m.Id == id);
            if (othellooption == null)
            {
                return NotFound();
            }
            OthelloOption = othellooption;
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

            othelloDb.Attach(OthelloOption).State = EntityState.Modified;

            try
            {
                await othelloDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OthelloOptionExists(OthelloOption.Id))
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
