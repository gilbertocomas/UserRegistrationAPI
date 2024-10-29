using Microsoft.AspNetCore.DataProtection;

namespace UserRegistrationAPI.Services
{
    public class DataProtectionService
    {
        private readonly IDataProtector _protector;

        public DataProtectionService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("Esta_Cadena_Sirve_Como_Mi_llave_para_Encriptar");
        }

        public string Protect(string input)
        {
            return _protector.Protect(input);
        }

        public string Unprotect(string input)
        {
            return _protector.Unprotect(input);
        }
    }
}
