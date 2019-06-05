using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public List<SelectListItem> Department { get; set; }

        public Employee employee { get; set; }

        protected string _connectionString;

        public CreateEmployeeViewModel()
        {

            Department = Department.GetAllDepartments()
                .Select(department => new SelectListItem()
                {
                    Text = department.name,
                    Value = department.id.ToString()
                })
                .ToList();

            Department.Insert(0, new SelectListItem
            {
                Text = "Choose a department",
                Value = "0"
            });

        }
    }
}
