using BangazonWorkforce.Models;
using BangazonWorkforce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Repositories
{
    public class DepartmentRepository
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

        //Get All Departments:
        public static List<Department> GetDepartments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            d.Id, d.Name, d.Budget, count(e.Id) TotalEmployees
                        FROM Department d
                        FULL JOIN Employee e on d.Id = e.DepartmentId 
                        group by d.Id, d.Name, d.Budget
                        ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Department> departments = new List<Department>();
                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Budget = reader.GetInt32(reader.GetOrdinal("Budget")),
                            TotalEmployees = reader.GetInt32(reader.GetOrdinal("TotalEmployees"))
                        };
                        departments.Add(department);
                    }
                        reader.Close();
                    return departments;
                }
            }
        }

        //Get Single Department (Details):


        //Get Single Department (for Delete):
        public static Department GetOneDepartment(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            d.Id, d.Name, d.Budget,
                            e.Id, e.FirstName, e.LastName, e.DepartmentId, e.IsSuperVisor
                        FROM Department d
                        LEFT JOIN Employee e ON e.DepartmentId = d.Id 
                        WHERE d.Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    Department Department = null;
                    if (reader.Read())
                    {
                        Department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Budget = reader.GetInt32(reader.GetOrdinal("Budget")),
                        };
                    }
                    reader.Close();
                    return Department;
                }
            }
        }

     //Create Department:
        public static void CreateDepartment(Department department)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Department
                (Name, Budget) VALUES
                (@name, @budget)";
                    cmd.Parameters.Add(new SqlParameter("@name", department.Name));
                    cmd.Parameters.Add(new SqlParameter("@budget", department.Budget));
                    cmd.ExecuteNonQuery();

                }
            }
        }

     //Delete Department:
        public static void DeleteDepartment(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Department WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}