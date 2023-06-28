using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EmployeeAPI.Models
{
    public class EmployeeBL
    {
        public void InsertEmployee(Employee emp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmpDB"].ToString();

            SqlConnection conn = new SqlConnection(connectionString);


            string query = "INSERT INTO Employee (FirstName, Lastname, Email, PhoneNumber, DOJ, Salary) " +
                   "VALUES (@FirstName, @Lastname, @Email, @PhoneNumber, @DOJ, @Salary)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = emp.FirstName;
            cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = emp.LastName;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = emp.Email;
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 200).Value = emp.PhoneNumber;
            cmd.Parameters.Add("@DOJ", SqlDbType.DateTime).Value = emp.DOJ;
            cmd.Parameters.Add("@Salary", SqlDbType.VarChar, 50).Value = emp.Salary;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public int GetTax(int salary)
        {
            double deduction = 0, tempsalary = salary;
            int count = 0;

            if (salary > 250000)
            {
                count = Convert.ToInt32(Math.Floor(Convert.ToDouble(salary / 250000)));
            }
            if (count > 4) count = 4;


            if (salary > 1000001)
            {
                tempsalary = salary - 1000000;
                double slabDeduction = tempsalary * 0.2;
                deduction += slabDeduction;
                tempsalary = 1000000;
                salary = 1000000;
                //Console.WriteLine("The Tax deducted 20% for the " + salary + " is " + slabDeduction);
            }
            if (salary >= 500001 && salary <= 1000000)
            {
                tempsalary = salary - 500000;
                double slabDeduction = tempsalary * 0.1;
                deduction += slabDeduction;
                tempsalary = 500000;
                salary = 500000;
                //Console.WriteLine("The Tax deducted 10% for the " + salary + " is " + slabDeduction);
            }

            if (salary >= 250001 && salary <= 500000)
            {
                tempsalary = salary - 250000;
                double slabDeduction = tempsalary * 0.05;
                deduction += slabDeduction;
                tempsalary = 250000;
                salary = 250000;
                //Console.WriteLine("The Tax deducted 5% for the " + salary + " is " + slabDeduction);
            }

            if (salary <= 250000)
            {
                deduction += 0;
            }

            Console.WriteLine("The Total tax deducted is " + deduction);
            return Convert.ToInt32(deduction);
        }
    }
}