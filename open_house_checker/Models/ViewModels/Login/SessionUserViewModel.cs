namespace open_house_checker.Models.ViewModels.Login
{
    public class SessionUserViewModel
    {
        public int id { get; set; }
        public string emp_num { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string role { get; set; }
        public bool isallowed { get; set; }
        public string area { get; set; }
    }
}
