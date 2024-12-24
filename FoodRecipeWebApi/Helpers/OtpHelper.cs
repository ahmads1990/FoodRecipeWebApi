using OtpNet;

namespace FoodRecipeWebApi.Helpers;

public static class OtpHelper
{
    public static string? GenerateRandomOtp(int digits = 6)
    {
        var key = KeyGeneration.GenerateRandomKey(20);
        var base32Secret = Base32Encoding.ToString(key);

        // Create a TOTP instance with the secret key
        var totp = new Totp(key, step: 30, totpSize: digits);

        var otp = totp.ComputeTotp();

        return otp;
    }
}
