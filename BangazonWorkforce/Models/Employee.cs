﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public Boolean IsSuperVisor { get; set; }
        public string Department { get; set; }
        //------------------Referenct to Department Name Ticket #6--------------------//
        public Department CurrentDepartment { get; set; }
        //-------------------Reference to Most Recent Computer Ticket #6-----------------//
        public Computer AssignedComputer { get; set; }
    }
}
