using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class OthelloOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Width { get; set; } = 8;
        public int Height { get; set; } = 8;
        public bool WhiteStarts { get; set; } = true;

        // ICollection - no foo[]
        public ICollection<OthelloGame>? OthelloGames { get; set; }

        public override string ToString()
        {
            return $"Board: {Width}x{Height}; WhiteStarts:{WhiteStarts}";
        }

    }
}
