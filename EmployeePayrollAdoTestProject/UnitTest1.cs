using EmployeePayrollADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeePayrollAdoTestProject
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepositry repository;
        EmployeeModel model;

        [TestInitialize]
        public void Setup()
        {
            repository = new EmployeeRepositry();
            model = new EmployeeModel();
        }
        [TestMethod]
        public void UpdateSalary()
        {
            try
            {
                string actual, expected;
                //Setting values to model object
                model.empId = 1;
                model.name = "lionel";
                model.basicPay = 50000;
                //Expected
                expected = "Updated";
                actual = repository.UpdateSalary(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [TestMethod]
        public void UpdateSalaryUsingStoredProcedure()
        {
            try
            {
                string actual, expected;
                //Setting values to model object
                model.empId = 2;
                model.name = "neymar";
                model.basicPay = 50000;
                //Expected
                expected = "Updated";
                actual = repository.UpdateSalary(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
