namespace open_house_checker.Models.ViewModels.Admin
{
    public class EmployeeReportViewModel
    {
        public int Id { get; set; }
        public string? emp_id_rh { get; set; }
        public string? emp_name { get; set; }
        public bool? emp_ischecked { get; set; }
        public DateTime? emp_check_date { get; set; }
        public int emp_total_visit_male { get; set; }
        public int emp_total_visit_female { get; set; }
        public int emp_total_visit_adult { get; set; }
        public int emp_total_visit_child { get; set; }
        public int emp_total_visits { get; set; }
    }
}
