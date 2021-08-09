﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

        /// <summary>
        /// Add A column in table
        /// </summary>
        public string AddIsActive()
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
                    //set command text to command object
                    command.CommandText = @"ALTER TABLE Employee ADD IsActive int NOT NULL default 1;";
                    int x = command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = $"Success";
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
        /// Update is active field
        /// </summary>
        public string ListForAudit(int id)
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
                    command.CommandText = @$"UPDATE Employee SET IsActive=0 WHERE EmployeeId={id}";
                    //Execute command
                     command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = $"Success";
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

        /// <summary>
        /// Retrive All data from table
        /// </summary>
      
        public string RetriveAllData()
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT c.CompanyId,c.CompanyName,emp.IsActive,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.EmpAddress,
                                p.BasicPay,p.TaxablePay,p.IncomeTax,p.NetPay,p.Deductions,d.DepartmentName
                                FROM Company AS c
                                INNER JOIN Employee AS emp ON c.CompanyId=emp.CompanyId AND emp.IsActive=1
                                INNER JOIN Payroll AS p ON p.EmployeeId = emp.EmployeeId
                                INNER JOIN EmployeeDepartment as EmpDept ON EmpDept.EmployeeId = emp.EmployeeId
                                INNER JOIN Department as d ON d.DeptId = EmpDept.DeptId;";
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
                        //Print deatials that are retrived
                        ShowDetails(result);

                    }
                    output = "Success";
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
            return output;
        }
        /// <summary>
        /// method to record time to retreive data without using thread
        /// </summary>
        /// <returns></returns>
        public string RetreiveAllDataWithoutUsingThread()
        {
            string output = string.Empty;
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                //start the stopwatch
                stopWatch.Start();
                RetriveAllData();
                //stop stopwatch
                stopWatch.Stop();
                Console.WriteLine($"Duration : {stopWatch.ElapsedMilliseconds} milliseconds");
                output = "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return output;
        }
        public void ShowDetails(SqlDataReader result)
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            EmployeeModel model = new EmployeeModel();
            //reatreive data and print details
            model.empId = Convert.ToInt32(result["EmployeeId"]);
            model.name = Convert.ToString(result["EmployeeName"]);
            model.basicPay = Convert.ToDouble(result["BasicPay"]);
            model.startDate = (DateTime)result["StartDate"];
            model.gender = Convert.ToChar(result["Gender"]);
            model.department = Convert.ToString(result["DepartmentName"]);
            model.phoneNumber = Convert.ToInt64(result["PhoneNumber"]);
            model.address = Convert.ToString(result["EmpAddress"]);
            model.deductions = Convert.ToDouble(result["Deductions"]);
            model.taxablePay = Convert.ToDouble(result["TaxablePay"]);
            model.incomeTax = Convert.ToDouble(result["IncomeTax"]);
            model.netPay = Convert.ToDouble(result["NetPay"]);
            model.companyId = Convert.ToInt32(result["CompanyId"]);
            model.companyName = Convert.ToString(result["CompanyName"]);
            model.isActive = Convert.ToInt32(result["IsActive"]);
            employeeModels.Add(model);
            Console.WriteLine($"{model.isActive},{model.empId},{model.name},{model.basicPay},{model.startDate},{model.gender},{model.department},{model.phoneNumber},{model.address},{model.deductions},{model.taxablePay},{model.incomeTax},{model.netPay},{model.companyId},{model.companyName}\n");
        }
    }

}

