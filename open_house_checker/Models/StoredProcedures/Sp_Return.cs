namespace open_house_checker.Models.StoredProcedures
{
    public class Sp_Return
    {
        public int Id { get; set; }
        public string id_rh { get; set; }
        public bool was_updated { get; set; }
        public string emp_name_check { get; set; }
        public int total_count { get; set; }
    }
}
