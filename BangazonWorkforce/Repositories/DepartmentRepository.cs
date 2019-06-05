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
        public static List<Department> GetAllDepartments()
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
    }
}