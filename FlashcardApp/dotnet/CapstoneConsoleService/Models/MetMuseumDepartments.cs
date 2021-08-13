using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class MetMuseumDepartments
    {
        public List<Department> Departments { get; set; }

        public MetMuseumDepartments()
        {
            Departments = new List<Department>();
        }
    }
}
