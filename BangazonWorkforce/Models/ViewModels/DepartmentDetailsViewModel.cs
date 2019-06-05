using BangazonWorkforce.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class DepartmentDetailsViewModel
    {


        [Display(Name = "Department Details:")]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Budget { get; set; }


        [Display(Name = "Department Team Members:")]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();


        public Department department { get; set; }

        //public DepartmentDetailsViewModel(int id)
        //{
        //    department = DepartmentRepository.GetOneDepartment(id);
        //    List<Employee> assignedStudents = DepartmentRepository.GetEmployeeByDepartment(id);

        //    numberOfStudentsCompleted = assignedStudents.Where(s => s.isComplete == true).Count();

        //    try
        //    {
        //        percentageOfStudentsCompleted = ((double)numberOfStudentsCompleted / (double)assignedStudents.Count()) * 100;
        //    }
        //    catch (Exception)
        //    {
        //        percentageOfStudentsCompleted = null;
        //    }



        //}


    }


}