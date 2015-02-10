using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Infinity_YAAB.Models.Domain
{
    public class ArmyList
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<Skills> Units = new List<Skills>();
        public int Points { get; set; }
        public int SWC { get; set; }
    }
}