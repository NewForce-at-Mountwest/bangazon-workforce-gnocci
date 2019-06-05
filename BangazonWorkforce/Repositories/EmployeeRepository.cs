using BangazonWorkforce.Models;
using BangazonWorkforce.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Repositories
{
    public class EmployeeRepository
    {

        private static IConfiguration _config;

        public static void SetConfig(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public static List<Employee> GetEmployees()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT e.Id,
                     e.FirstName,
                    e.LastName,
                    d.[Name] AS 'department'
                    FROM Employee e FULL JOIN Department d ON e.DepartmentId = d.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Department = reader.GetString(reader.GetOrdinal("department"))
                        };

                        employees.Add(employee);
                    }

                    reader.Close();

                    return employees;
                }
            }

        }

        public static Employee GetOneEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id,
                     e.FirstName,
                    e.LastName,
e.IsSuperVisor,
                    d.[Name] AS 'department', 
d.Id AS'DepartmentId'
                    FROM Employee e FULL JOIN Department d ON e.DepartmentId = d.Id
                    WHERE e.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee Employee = null;

                    if (reader.Read())
                    {
                        Employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            IsSuperVisor = reader.GetBoolean(reader.GetOrdinal("IsSuperVisor")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            Department = reader.GetString(reader.GetOrdinal("department"))
                        };
                    }
                    reader.Close();

                    return Employee;
                }
            }

        }

        public static void UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Update the student's basic info
                    string command = @"UPDATE Employee
                                            SET FirstName=@FirstName, 
                                            LastName=@LastName, 
                                            IsSuperVisor=@IsSuperVisor, 
                                            DepartmentId=@DepartmentId
                                            WHERE Id = @id";
                    cmd.CommandText = command;
                    cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@IsSuperVisor", employee.IsSuperVisor));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", employee.DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateEmployee(CreateEmployeeViewModel model)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Employee
                (FirstName, LastName, IsSuperVisor, DepartmentId) VALUES
                (@FirstName, @LastName, @IsSuperVisor, @DepartmentId)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", model.employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", model.employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@IsSuperVisor", model.employee.IsSuperVisor));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", model.employee.DepartmentId));
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}