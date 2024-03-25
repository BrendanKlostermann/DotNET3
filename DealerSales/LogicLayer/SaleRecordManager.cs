using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;


namespace LogicLayer
{
    public class SaleRecordManager : ISaleRecordManager
    {
        public int AddSaleRecord(SaleRecord record)
        {
            throw new NotImplementedException();
        }

        public List<SaleRecord> GetAllSaleRecord()
        {
            throw new NotImplementedException();
        }

        public SaleRecord GetSaleRecordByID(int recordID)
        {
            throw new NotImplementedException();
        }

        public List<SaleRecord> GetSaleRecordsByCustomerID(int customerID)
        {
            throw new NotImplementedException();
        }
    }
}
