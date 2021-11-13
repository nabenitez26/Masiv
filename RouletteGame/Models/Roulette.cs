using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Models
{
    [Serializable]
    public class Roulette
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; } = false;
        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public List<BetCustomer> Bets { get; set; }

        public static explicit operator Roulette(CacheValue<Roulette> v)
        {
            throw new NotImplementedException();
        }
    }
}
