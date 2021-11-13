using EasyCaching.Core;
using RouletteGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Repositories
{
    public class RouletteRepository: IRouletteRepository
    {
        private const string tableRoulette = "Roulette_";
        private readonly IEasyCachingProvider _easyCachingProvider;
        public RouletteRepository(IEasyCachingProvider easyCachingProvider)
        {
            _easyCachingProvider = easyCachingProvider;
        }
        public async Task<bool> Save(Roulette roulette) 
        {
            try
            {
                if (roulette.Id == 0)
                {
                    int countRoulette = _easyCachingProvider.GetCount(tableRoulette);
                    roulette.Id = countRoulette++;
                }
                await _easyCachingProvider.SetAsync(tableRoulette + roulette.Id, roulette, TimeSpan.FromDays(1));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<Roulette> GetById(int Id)
        {
            try
            {
                var  roulette = await _easyCachingProvider.GetAsync<Roulette>(tableRoulette + Id);
                return roulette.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<Roulette>> GetAll()
        {
            try
            {
                var roulette = await _easyCachingProvider.GetByPrefixAsync<Roulette>(tableRoulette);
                return new List<Roulette>(roulette.Select(x => x.Value.Value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
