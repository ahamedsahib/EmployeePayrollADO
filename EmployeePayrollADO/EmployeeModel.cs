﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollADO
{
    public class EmployeeModel
    {
        public int empId { get; set; }
        public string name { get; set; }
        public double basicPay { get; set; }
        public DateTime startDate { get; set; }
        public char gender { get; set; }
        public string department { get; set; }
        public Int64 phoneNumber { get; set; }
        public string address { get; set; }
        public double deductions { get; set; }
        public double taxablePay { get; set; }
        public double incomeTax { get; set; }
        public double netPay { get; set; }
    }
}
