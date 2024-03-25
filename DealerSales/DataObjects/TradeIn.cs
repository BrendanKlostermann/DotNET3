using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TradeIn
    {
        public int TradeInID { get; set; }
        public int SaleID { get; set; }
        public decimal TradeInValue { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }


        public TradeIn()
        {

        }
    }
}
