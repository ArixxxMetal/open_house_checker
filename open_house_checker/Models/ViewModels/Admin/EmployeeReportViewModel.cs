namespace open_house_checker.Models.ViewModels.Admin
{
    public class EmployeeReportViewModel
    {
        public int Id { get; set; }
        public string? emp_id_rh { get; set; }
        public string? emp_name { get; set; }
        public string? emp_job_id { get; set; }
        public string? emp_job { get; set; }
        public string? emp_department { get; set; }
        public string? emp_dep_id { get; set; }
        public string? emp_area { get; set; }
        public string? emp_bum { get; set; }
        public bool? emp_ischecked { get; set; }
        public DateTime? emp_check_date { get; set; }
        public int emp_total_visits { get; set; }
    }
}
