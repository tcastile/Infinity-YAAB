using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity_YAAB.Models.Domain
{
    public class Statistics
    {
        public int ID { get; set; }
        public int UnitID { get; set; } 
        public int Move1 { get; set; }
        public int Move2 { get; set; }
        public int CloseCombat { get; set; }
        public int BalisticScore { get; set; }
        public int Physical { get; set; }
        public int Willpower { get; set; }
        public int Armor { get; set; }
        public int BiotechShield { get; set; }
    }
}