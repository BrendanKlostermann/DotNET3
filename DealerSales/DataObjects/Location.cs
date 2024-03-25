using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Location
    {

        public int LocationID { get; set; }
        public int ZipCode { get; set; }
        public string StreetAddress { get; set; }
        public string LocationName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
