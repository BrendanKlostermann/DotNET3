﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ITradeInAccessor
    {
        TradeIn SelectTradeInBySaleID(int saleID);
        int InsertTradeIn(TradeIn trade);
        int DeleteTradeIn(int tradeID);
    }
}
