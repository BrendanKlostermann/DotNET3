using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayerInterfaces
{
    public interface IZipCodeManager
    {
        List<ZipCode> GetAllZipCodes();

    }
}
