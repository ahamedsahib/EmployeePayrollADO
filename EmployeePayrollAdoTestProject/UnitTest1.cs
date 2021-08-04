using EmployeePayrollADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
       
    }
}
