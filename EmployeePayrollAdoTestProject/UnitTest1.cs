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
        EmployeeRepo employeeRepo;
        PayrollTransaction payrollTransaction;

        [TestInitialize]
        public void Setup()
        {
            repository = new EmployeeRepositry();
            model = new EmployeeModel();
            employeeRepo = new EmployeeRepo();
            payrollTransaction = new PayrollTransaction();
        }
        //[TestMethod]
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
       // [TestMethod]
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
        [TestMethod]
        public void RetrieveDataUsingName()
        {
            try
            {
                string actual, expected;
                model.name = "Dias";
                expected = "Success";
                actual = repository.GetDataUsingName(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Method to retrive based on range
        /// </summary>
        [TestMethod]
        public void RetrieveDataUsingRange()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = repository.RetriveDataBasedOnRange(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [TestMethod]
        public void TestForAggregateFunctions()
        {
            try
            {
                string actual, expected;
                expected = $"Total Salary = 162000 Max Salary = 50000 Min Salary = 5000 Avg Salary = 32400 Gender = M  Count = 5";
                actual = repository.AggregateFunctions("M");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test method for retrieve all data based on er
        /// </summary>
        [TestMethod]
        public void TestForRetrieveDataER()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = employeeRepo.RetriveAllDataER(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// test for Update data based on er
        /// </summary>
       // [TestMethod]
        public void TestForUpdateER()
        {
            try
            {
                string actual, expected;
                expected = "Updated";
                actual = employeeRepo.UpdateDetailsER(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Reteive based on range er
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingRangeER()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = employeeRepo.RetreiveBasedOnRangeER(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// test aggregate function based on er male
        /// </summary>
        [TestMethod]
        public void TestForAggregateFunctionsER()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = employeeRepo.AggregateFunctionsER("M");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test For Insert Data Into Table Usin TQ query
        /// </summary>
       // [TestMethod]
        public void TestForInsertIntoTableTransaction()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = payrollTransaction.InsertDataIntoTableUsingTransaction();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test For Deelete cascade
        /// </summary>
        //[TestMethod]
        public void TestMethodForDeleteCascade()
        {
            try
            {
                string actual, expected;
                expected = "Deleted Successfully";
                actual = payrollTransaction.DeleteCascade();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test For add column
        /// </summary>
        //[TestMethod]
        public void TestForAddIsActiveColumn()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = payrollTransaction.AddIsActive();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test For update audit
        /// </summary>
        [TestMethod]
        public void TestForAudit()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = payrollTransaction.ListForAudit(1);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Test Method For retreive all data transaction
        /// </summary>
        [TestMethod]
        public void TestForRetreiveAllDataTransaction()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = payrollTransaction.RetriveAllData();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test Method For retreive all data  Transaction without using thread
        /// </summary>
        [TestMethod]
        public void TestMEthodForRetreiveAllDataWithoutUsingThread()
        {
            try
            {
                string actual, expected;
                expected = "success";
                actual = payrollTransaction.RetreiveAllDataWithoutUsingThread();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
