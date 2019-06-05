using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace BangazonWorkforce.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public List<SelectListItem> Departments { get; set; }

        public Employee employee { get; set; }

        // Connection to the database
        protected string _connectionString;

        public CreateEmployeeViewModel()
        {

            Departments = DepartmentRepository.GetAllDepartments()
                .Select(department => new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.Id.ToString()

                })
                .ToList();

            // Add an option with instructiosn for how to use the dropdown
            Departments.Insert(0, new SelectListItem
            {
                Text = "Choose a cohort",
                Value = "0"
            });

        }

    }
}
