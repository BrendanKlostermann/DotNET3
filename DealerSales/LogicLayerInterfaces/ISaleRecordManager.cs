using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ISaleRecordManager
    {
        List<SaleRecord> GetAllSaleRecord();
        int AddSaleRecord(SaleRecord record);
        List<SaleRecord> GetSaleRecordsByCustomerID(int customerID);
        SaleRecord GetSaleRecordByID(int recordID);

    }
}
