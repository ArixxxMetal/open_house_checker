using System;
using System.Collections.Generic;

namespace open_house_checker.Models.DB
{
    public partial class EmployeeVisitor
    {
        public int Id { get; set; }
        public string? VisitType { get; set; }
        public string? VisitSex { get; set; }
        public string? VisitAge { get; set; }
        public int? VisitEmpId { get; set; }

        public virtual EmployeeTable? VisitEmp { get; set; }
    }
}
