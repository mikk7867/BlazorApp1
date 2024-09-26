using Microsoft.AspNetCore.DataProtection;

namespace BlazorApp1.Codes
{
    public class SymetricEncrypter
    {
        private readonly IDataProtector dataProtector;

        public SymetricEncrypter(IDataProtectionProvider protector)
        {
            dataProtector = protector.CreateProtector("NielsDetGaarRetHurtigt");
        }

        public string Protect(string input) =>
            dataProtector.Protect(input);
        public string UnProtect(string input) =>
            dataProtector.Unprotect(input);
    }
}
