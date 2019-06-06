using Microsoft.AspNetCore.Mvc.Rendering;
using BangazonWorkforce.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class EditEmployeeViewModel
    {
        public List<SelectListItem> Departments { get; set; }

        public Employee employee { get; set; }


        public EditEmployeeViewModel() { }

        public EditEmployeeViewModel(int employeeId)
        {

            employee = EmployeeRepository.GetOneEmployee(employeeId);
            Departments = DepartmentRepository.GetAllDepartments()
                .Select(department => new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.Id.ToString()

                })
                .ToList();
            Departments.Insert(0, new SelectListItem
            {
                Text = employee.Department,
                Value = employee.DepartmentId.ToString()
            });

        }

    }
}