namespace SOWEDANE.Models
{
    public class UserOtp
    {
        public int Id { get; set; }

        public string LoginName { get; set; }

        public string  OTP { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
