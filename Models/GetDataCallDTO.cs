using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3.Models
{
    public class GetDataCallDTO
    {
        public List<CardColor> card_colors { get; set; }
        public List<WinningSet> winning_Sets { get; set; }
    }
}
