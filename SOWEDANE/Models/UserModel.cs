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

        public String GeneratedSalt { get; set; }
    }
}
