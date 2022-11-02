using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloOptions
{
    public class IndexModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public IndexModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OthelloOption> OthelloOption { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OthelloOptions != null)
            {
                OthelloOption = await _context.OthelloOptions.ToListAsync();
            }
        }
    }
}
