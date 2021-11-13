using RouletteGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.DTO
{
    public class BetResponse
    {
        public short NumberRoulette{ get; set; }
        public List<BetCustomer> betCustomers { get; set; }
    }
}
