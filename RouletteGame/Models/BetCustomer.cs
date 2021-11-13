using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Models
{
    [Serializable]
    public class BetCustomer
    {
        public int Number { get; set; }
        public double Money { get; set; }
        public int UserId { get; set; }
        public bool IsWinner{ get; set; }
        public double TotalMoney { get; set; }

    }
}
