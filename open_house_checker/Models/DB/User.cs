using System;
using System.Collections.Generic;

namespace open_house_checker.Models.DB
{
    public partial class User
    {
        public int Id { get; set; }
        public string? UsrIdRh { get; set; }
        public string? UsrName { get; set; }
        public string? UsrPassword { get; set; }
        public bool? UsrIsactive { get; set; }
        public DateTime? UsrCreateDate { get; set; }
    }
}
