using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGameStates
{
    public class IndexModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public IndexModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OthelloGameState> OthelloGameState { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OthelloGamesStates != null)
            {
                OthelloGameState = await _context.OthelloGamesStates
                .Include(o => o.OthelloGame).ToListAsync();
            }
        }
    }
}
