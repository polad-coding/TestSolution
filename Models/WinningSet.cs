using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3.Models
{
    public class WinningSet
    {
        public int id { get; set; }
        public int points { get; set; }
        public List<int> set { get; set; }
    }
}
