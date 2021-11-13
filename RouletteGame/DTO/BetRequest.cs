using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.DTO
{
    public class BetRequest
    {
        /// <summary>
        /// range 0 - 36 Number, 37 => Black, 38 => Red
        /// </summary>
        [Range(0, 38)]
        public int Number { get; set; }
        [Range(1, maximum: 10000)]
        public double Money { get; set; }
    }
}
