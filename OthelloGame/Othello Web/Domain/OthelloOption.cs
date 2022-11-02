using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Othello_Web.Domain
{
    public class OthelloOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Width { get; set; } = 8;
        public int Height { get; set; } = 8;
        public string CurrentPlayer { get; set; } = "Black";

        public ICollection<OthelloGame>? OthelloGames { get; set; }


        public override string ToString()
        {
            return $"Board: {Width}x{Height}; Current Player:{CurrentPlayer}";
        }

    }
}
