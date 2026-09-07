using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Project_Management_System.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }


        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public List<Project> Projects { get; set; } = new List<Project>();


    }
}
