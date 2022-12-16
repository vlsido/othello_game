using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Db
{
    public class GameRepositoryDb : IGameRepository
    {
        private readonly AppDbContext _dbContext;

        public GameRepositoryDb(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Name { get; } = "DB";

        public List<OthelloGame> GetAll()
        {
            throw new NotImplementedException();
        }

        public OthelloGame GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public OthelloGame AddGame(OthelloGame game)
        {
            throw new NotImplementedException();
        }
    }
}
