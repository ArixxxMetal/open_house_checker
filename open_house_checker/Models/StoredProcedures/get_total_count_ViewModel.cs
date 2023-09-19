namespace open_house_checker.Models.StoredProcedures
{
    public class get_total_count_ViewModel
    {
        public int Id { get; set; }
        public int emp_count { get; set; }
        public int visit_count { get; set; }
        public int visit_adult_count { get; set; }
        public int visit_child_count { get; set; }
        public int visit_sex_m_count { get; set; }
        public int visit_sex_f_count { get; set; }
        public int total_count { get; set; }
    }
}
