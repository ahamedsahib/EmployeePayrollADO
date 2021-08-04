﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollADO
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_service; Integrated Security = True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Retrive All data from table
        /// </summary>
 
        public string RetriveAllDataER(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT c.CompanyId,c.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.EmpAddress,
                                p.BasicPay,p.TaxablePay,p.IncomeTax,p.NetPay,p.Deductions,d.DeptartmentName
                                FROM Company AS c
                                INNER JOIN Employee AS emp ON c.CompanyId=emp.CompanyId
                                INNER JOIN Payroll AS p ON p.EmployeeId = emp.EmployeeId
                                INNER JOIN EmployeeDepartment ON EmpDept.EmployeeId = emp.EmployeeId
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
                        PrintDetails(result, model);

                    }
                    output = "Success";
                }
                else
                {
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
        /// Update details on table
        /// </summary>

        public string UpdateDetailsER(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = "UPDATE Payroll set BasicPay=65000 FROM Payroll AS P INNER JOIN Employee AS e ON p.EmployeeId=e.EmployeeId WHERE e.EmployeeName='neymar';";
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
                output = ex.Message;
            }
            finally
            {
                //close connection
                connection.Close();
            }
            return output;
        }

        /// <summary>
        /// retrieve based on range
        /// </summary>
        public string RetreiveBasedOnRangeER(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT c.CompanyID,c.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender
                                FROM Company AS c 
                                INNER JOIN Employee AS emp 
                                ON c.CompanyId = emp.CompanyId AND emp.StartDate BETWEEN ('2010-05-01') AND getdate();";
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
                        model.empId = Convert.ToInt32(result["EmployeeId"]);
                        model.name = Convert.ToString(result["EmployeeName"]);
                        model.startDate = (DateTime)result["StartDate"];
                        model.gender = Convert.ToChar(result["Gender"]);
                        model.phoneNumber = Convert.ToInt64(result["PhoneNumber"]);
                        model.companyId = Convert.ToInt32(result["CompanyId"]);
                        model.companyName = Convert.ToString(result["CompanyName"]);
                        Console.WriteLine($"Id- {model.empId},Name - {model.name}, startDate - {model.startDate},Gender - {model.gender},phone number - {model.phoneNumber},companyId - {model.companyId},CompanyName - {model.companyName}\n");
                    }
                    output = "Success";
                }
                else
                {
                    
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
        /// Aggregate functions of salary of gender
        /// </summary>
        public string AggregateFunctionsER(String gender)
        {
            string output = string.Empty;
            try
            {
                using (this.connection)
                {
                    string query = @$"SELECT SUM(p.BasicPay),MAX(p.BasicPay),MIN(p.BasicPay),AVG(p.BasicPay),emp.Gender,COUNT(*) FROM Employee AS emp INNER JOIN Payroll as p ON emp.EmployeeId = p.EmployeeId WHERE emp.Gender ='{gender}' GROUP BY emp.Gender";
                    //sqlCommand initialised 
                    SqlCommand command = new SqlCommand(query, connection);
                    //open the connection
                    connection.Open();
                    //Sql data reader- using execute reader returns object for resultset
                    SqlDataReader result = command.ExecuteReader();

                    //checking result set has rows are not
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Console.WriteLine($"Total Salary = {result[0]}\n Max Salary = {result[1]}\n Min Salary = {result[2]}\n Avg Salary = {result[3]}\n Gender = {result[4]} \n Count = {result[5]}\n");
                        }
                        //close the reader object
                        result.Close();
                    }

                }
                output = "Success";

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                output = "Unsuccessfull";
            }
            finally
            {
                //close the connection
                connection.Close();
            }
            return output;
        }


        /// <summary>
        /// Print details
        /// </summary>
        public void PrintDetails(SqlDataReader result, EmployeeModel model)
        {
            //reatreive adata and print details
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
            Console.WriteLine($"{model.empId},{model.name},{model.basicPay},{model.startDate},{model.gender},{model.department},{model.phoneNumber},{model.address},{model.deductions},{model.taxablePay},{model.incomeTax},{model.netPay},{model.companyId},{model.companyName}\n");
        }

    }
}
