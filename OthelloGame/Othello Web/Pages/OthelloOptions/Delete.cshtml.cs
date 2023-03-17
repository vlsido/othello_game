using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Othello_Web.Pages_OthelloOptions
{
    public class DeleteModel : PageModel
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

            var othellooption = await othelloDb.OthelloOptions.FirstOrDefaultAsync(m => m.Id == id);

            if (othellooption == null)
            {
                return NotFound();
            }
            else 
            {
                OthelloOption = othellooption;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (id == null || othelloDb.OthelloOptions == null)
            {
                return NotFound();
            }
            var othellooption = await othelloDb.OthelloOptions.FindAsync(id);

            if (othellooption != null)
            {
                OthelloOption = othellooption;
                othelloDb.OthelloOptions.Remove(OthelloOption);
                await othelloDb.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
