using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Othello_Web.Pages_OthelloGames
{
    public class IndexModel : PageModel
    {
        

        public IList<OthelloGame> OthelloGame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (othelloDb.OthelloGames != null)
            {
                OthelloGame = await othelloDb.OthelloGames
                .Include(o => o.OthelloOption).ToListAsync();
            }
        }
    }
}
