using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages_OthelloGames
{
    public class DetailsModel : PageModel
    {
        private readonly Othello_Web.Data.ApplicationDbContext _context;

        public DetailsModel(Othello_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public OthelloGame OthelloGame { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OthelloGames == null)
            {
                return NotFound();
            }

            var othellogame = await _context.OthelloGames.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
