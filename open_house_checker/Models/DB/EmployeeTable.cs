using System;
using System.Collections.Generic;

namespace open_house_checker.Models.DB
{
    public partial class EmployeeTable
    {
        public EmployeeTable()
        {
            EmployeeVisitors = new HashSet<EmployeeVisitor>();
        }

        public int Id { get; set; }
        public string? EmpIdRh { get; set; }
        public string? EmpName { get; set; }
        public string? EmpJobId { get; set; }
        public string? EmpJob { get; set; }
        public string? EmpDepartment { get; set; }
        public string? EmpDepId { get; set; }
        public string? EmpArea { get; set; }
        public string? EmpBum { get; set; }
        public bool? EmpIschecked { get; set; }
        public DateTime? EmpCheckDate { get; set; }

        public virtual ICollection<EmployeeVisitor> EmployeeVisitors { get; set; }
    }
}
