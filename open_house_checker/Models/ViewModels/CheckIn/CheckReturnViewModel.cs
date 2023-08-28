namespace open_house_checker.Models.ViewModels.CheckIn
{
    public class CheckReturnViewModel
    {
        public int Id { get; set; }
        public string Idrh { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public bool Ischecked { get; set; }
        public DateTime? CheckDate { get; set; }
        public string isalreadycheck { get; set; }
        public string totalchecked { get; set; }
    }
}
