using System;
using Backend.API.Models;

namespace Backend.API.Services

{
    public class OtpService
    {
        public string GenerateOTP(int length = 6)
        {
            var random = new Random();
            string otp = "";
            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 10);
            }
            return otp;
        }
    }
}
