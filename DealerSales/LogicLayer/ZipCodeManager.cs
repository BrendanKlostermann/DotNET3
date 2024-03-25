using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class ZipCodeManager : IZipCodeManager
    {
        ZipCode _zipCode = null;
        List<ZipCode> _zipCodes = null;

        public List<ZipCode> GetAllZipCodes()
        {
            ZipCodeAccessor zipCodeAccessor = new ZipCodeAccessor();
            _zipCodes = zipCodeAccessor.RetrieveAllZipCodes();
            return _zipCodes;
        }
    }
}
