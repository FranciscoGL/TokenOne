using System.Web;

[assembly: PreApplicationStartMethod(typeof(TokenApp.Startup), "Start")]

namespace TokenApp
{
    public class Startup
    {
        public static void Start()
        {
            SecuritySetting.ClientId = TokenSetting.Default.ClientId;

            SecuritySetting.SecurityKey = TokenSetting.Default.SecurityKey;

            SecuritySetting.TokenIssuerName = TokenSetting.Default.TokenIssuerName;
        }
    }
}
