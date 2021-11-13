using RouletteGame.DTO;
using RouletteGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Services
{
    public interface IRouletteService
    {
        public Task<Roulette> AddNewRoulette();
        public Task<bool> OpenRoulette(int Id);
        public Task<bool> Bet(int Id, BetCustomer betCustomer);
        public Task<BetResponse> CloseRoulette(int Id);
        public Task<List<Roulette>> GetAll();
    }
}
