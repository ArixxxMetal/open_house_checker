namespace open_house_checker.Models.ViewModels.Admin
{
    public class EmployeeVisitorViewModel
    {
        public int Id { get; set; }
        public string? visit_type { get; set; }
        public string? visit_sex { get; set; }
        public string? visit_age { get; set; }
        public int? visit_emp_id { get; set; }
        public DateTime? visit_check_date { get; set; }
        public string? emp_id_rh { get; set; }
        public string? emp_name { get; set; }
    }
}
