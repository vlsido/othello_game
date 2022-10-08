using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    [Serializable]
    public class BadPlayerException : Exception
    {
        public string PlayerColor { get; } = null!;
        public BadPlayerException() { }

        public BadPlayerException(string message)
            : base(message) { }

        public BadPlayerException(string message, Exception inner)
            : base(message, inner) { }

        public BadPlayerException(string message, string playerColor)
            : this(message)
        {
            PlayerColor = playerColor;
        }
    }
}
