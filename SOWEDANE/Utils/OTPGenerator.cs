namespace SOWEDANE.Utils
{
    public class OTPGenerator
    {

        public static String generateOTP()
        {
            // Define the length of the OTP
            var otpLength = 6;

            // Generate a random numeric OTP
            var otp = string.Empty;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < otpLength; i++)
            {
                otp += Math.Floor(random.NextDouble() * 10);
            }

            return otp;
        }
    }
}
