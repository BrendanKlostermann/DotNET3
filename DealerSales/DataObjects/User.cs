﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? LocationID { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; set; }
    }
}
