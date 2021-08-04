using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollADO
{
    public class EmployeeRepositry
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_service; Integrated Security = True;";
            //SqlConnection
       SqlConnection connection = new SqlConnection(connectionString);

        //RetriveAllData
        public void GetEmployeeData()
        {
            try
            {
                //Object for employee class
                EmployeeModel model = new EmployeeModel();

                //Qurey to retreive data from table
                string query = "SELECT * FROM employee_payroll";
                SqlCommand command = new SqlCommand(query, connection);
                //Open Connection
                this.connection.Open();
                //Returns object of result set
                SqlDataReader result = command.ExecuteReader();
                //Check Result set has rows or not
                if (result.HasRows)
                {
                    //Parse untill  rows are null
                    while (result.Read())
                    {
                        model.empId = Convert.ToInt32(result["id"]);
                        model.name = Convert.ToString(result["name"]);
                        model.basicPay = Convert.ToDouble(result["BasicPay"]);
                        model.startDate = (DateTime)result["startDate"];
                        model.gender = Convert.ToChar(result["Gender"]);
                        model.department = Convert.ToString(result["department"]);
                        model.phoneNumber = Convert.ToInt64(result["Phone"]);
                        model.address = Convert.ToString(result["Address"]);
                        model.deductions = Convert.ToDouble(result["Deductions"]);
                        model.taxablePay = Convert.ToDouble(result["TaxablePay"]);
                        model.incomeTax = Convert.ToDouble(result["IncomeTax"]);
                        model.netPay = Convert.ToDouble(result["NetPay"]);
                        Console.WriteLine($"{model.empId},{model.name},{model.basicPay},{model.startDate},{model.gender},{model.department},{model.phoneNumber},{model.address},{model.deductions},{model.taxablePay},{model.incomeTax},{model.netPay}");

                    }
                }
                else
                {
                    Console.WriteLine("No Records in the table");
                }
                //close result set
                result.Close();
            }
            catch (Exception ex)
            {
                //handle exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }

        }
        public string UpdateSalary(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = "UPDATE employee_payroll set BasicPay=50000 WHERE name='lionel';";
                SqlCommand command = new SqlCommand(query, connection);
                //Open Connection
                this.connection.Open();
                //Returns numbers of rows updated
                int result = command.ExecuteNonQuery();
                //Check Result set is greater or equal to 1
                if (result >= 1)
                {
                    output = "Updated";
                }
                else
                {
                    output = "Not Updated";
                }

            }
            catch (Exception ex)
            {
                //handle exception
                //Console.WriteLine(ex.Message);
                return ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return output;

        }

    }
}
