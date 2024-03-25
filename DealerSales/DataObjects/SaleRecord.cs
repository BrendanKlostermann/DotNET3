using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SaleRecord
    {
        public int SaleID { get; set; }
        public int LocationID { get; set; }
        public int PayTypeID { get; set; }
        public int EmployeeID {get;set;}
        public int VehicleID { get; set; }
        public bool TradeIn { get; set; }
        public decimal SalePrice { get; set; }



        public SaleRecord()
        {

        }

    }
}
