using RouletteGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Repositories
{
    public interface IRouletteRepository
    {
        public Task<bool> Save(Roulette roulette);
        public Task<Roulette> GetById(int Id);
        public Task<List<Roulette>> GetAll();
    }
}
