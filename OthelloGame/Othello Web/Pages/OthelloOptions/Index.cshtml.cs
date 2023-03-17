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
    public class IndexModel : PageModel
    {
        

        public IList<OthelloOption> OthelloOption { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;
            var othelloDb = new AppDbContext(options);
            if (othelloDb.OthelloOptions != null)
            {
                OthelloOption = await othelloDb.OthelloOptions.ToListAsync();
            }
        }
    }
}
