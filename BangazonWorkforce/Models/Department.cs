﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Budget { get; set; }

        //---------------List of Employees Ticket #7-------------//

        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

    }
}
