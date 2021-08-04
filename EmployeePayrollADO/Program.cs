using System;

namespace EmployeePayrollADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ADO");
            EmployeeRepositry employee = new EmployeeRepositry();
            employee.GetEmployeeData();
        }
    }
}
