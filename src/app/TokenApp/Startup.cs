using System.Web;

[assembly: PreApplicationStartMethod(typeof(TokenApp.Startup), "Start")]

namespace TokenApp
{
    public class Startup
    {
        public static void Start()
        {
            SecuritySetting.SecurityKey = TokenSetting.Default.SecurityKey;

            SecuritySetting.AudienceAddress = TokenSetting.Default.AudienceAddress;

            SecuritySetting.TokenIssuerName = TokenSetting.Default.TokenIssuerName;
        }
    }
}
