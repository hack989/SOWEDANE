using Microsoft.AspNetCore.Mvc;
using SOWEDANE.EntityFrameworkContext;
using SOWEDANE.Models;
using SOWEDANE.Utils;
using System.Configuration;

namespace SOWEDANE.Controllers
{
    public class UserOtpController:Controller
    {

        private readonly UserOtpDbContext userOtpDbContext;
      
        private readonly IConfiguration configuration;
        public UserOtpController(IConfiguration configuration, UserOtpDbContext userOtpDbContext)
        {
            this.configuration = configuration;
            this.userOtpDbContext = userOtpDbContext;
        }

        public string GenerateOtp(string LoginName)
        {
            var existingOtp = this.userOtpDbContext.UserOtps.Where(x => x.LoginName == LoginName).FirstOrDefault();
            var expirationinSec = this.configuration.GetValue<int>("OTPExpirationTimeInSec", 30);
            var otp= OTPGenerator.generateOTP(); 
            if (existingOtp != null)
            {
                existingOtp.OTP = otp;
                existingOtp.ExpiresOn = DateTime.Now.AddSeconds(expirationinSec);
                this.userOtpDbContext.SaveChanges();
                return otp;
            }
            UserOtp userOtp = new UserOtp();
            userOtp.LoginName = LoginName;
            userOtp.OTP = otp;
          //  var expirationinSec=this.configuration.GetValue<int>("OTPExpirationTimeInSec", 30);
            userOtp.ExpiresOn = DateTime.Now.AddSeconds(expirationinSec);
            this.userOtpDbContext.Add(userOtp);
            this.userOtpDbContext.SaveChanges();
            return userOtp.OTP;
        }

        [HttpPost]
        public JsonResult validateOtp(string loginName, string otp)
        {

            var userOtp = this.userOtpDbContext.UserOtps.Where(x => x.LoginName == loginName).FirstOrDefault();
            var success = userOtp != null && userOtp.OTP == otp && userOtp.ExpiresOn > DateTime.Now;
            var jsonResult = new { success };
            return new JsonResult(jsonResult);
        
        }

        public ViewResult Success() 
        {
            return View("Success");
        }


    }
}
