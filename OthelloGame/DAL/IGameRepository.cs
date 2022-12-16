using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public interface IGameRepository
    {
        List<OthelloGame> GetAll();
        OthelloGame GetGame(int id);
        OthelloGame AddGame(OthelloGame game);
    }

}
