using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOWEDANE.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String LoginName { get; set; }
        public String PhoneNumber { get; set; }
        public String Password { get; set; }

        public String Email { get; set; }

        //[NotMapped]
        public String City { get; set; }

        [NotMapped]
        public List<SelectListItem> CityList { get; set; }

        public String GeneratedSalt { get; set; }
    }
}
