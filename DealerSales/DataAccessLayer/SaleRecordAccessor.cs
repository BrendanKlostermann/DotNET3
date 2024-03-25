using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SaleRecordAccessor : ISaleRecordAccessor
    {
        public int InsertSaleRecord(SaleRecord record)
        {
            throw new NotImplementedException();
        }

        public List<SaleRecord> SelectAllSaleRecords()
        {
            List<SaleRecord> sales = new List<SaleRecord>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_all_sales";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SaleRecord sale = new SaleRecord();
                        sale.SaleID = reader.GetInt32(0);
                        sale.LocationID = reader.GetInt32(1);
                        sale.PayTypeID = reader.GetInt32(2);
                        sale.EmployeeID = reader.GetInt32(3);
                        sale.VehicleID = reader.GetInt32(4);
                        sale.TradeIn = reader.GetBoolean(5);
                        sale.SalePrice = reader.GetDecimal(6);

                        sales.Add(sale);

                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Could not locate sales records", ex);
            }
            finally
            {
                conn.Close();
            }

            return sales;

        }

        public SaleRecord SelectSaleRecordByID(int recordID)
        {
            throw new NotImplementedException();
        }

        public List<SaleRecord> SelectSaleRecordsByCustomerID(int customerID)
        {
            throw new NotImplementedException();
        }
    }
}
