using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollADO
{
    public class PayrollTransaction
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_service; Integrated Security = True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Method Insert Into Table Using Transaction
        /// </summary>
        public string InsertDataIntoTableUsingTransaction()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"INSERT INTO Employee VALUES (3,'Rocky',8765432240,'Chennai','2020-09-21','M');";
                    //Execute command
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO Payroll(EmployeeId,BasicPay) VALUES (5,37000);";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET Deductions =(BasicPay*20)/100 WHERE EmployeeId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET TaxablePay =(BasicPay-Deductions) WHERE EmployeeId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET IncomeTax =(TaxablePay*10)/100 WHERE EmployeeId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET NetPay =(BasicPay-IncomeTax) WHERE EmployeeId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO EmployeeDepartment VALUES(5,2);";
                    command.ExecuteNonQuery();
                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }
       
        /// <summary>
        /// Delete cascade
        /// </summary>
        public string DeleteCascade()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    //set query to perform delete operation
                    command.CommandText = @"delete from Employee where EmployeeId=5";
                    //Execute command
                    int x = command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = "Deleted Successfully";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }
    }
    
}
