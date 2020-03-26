using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalClaimT.acceso
{
    public interface ISOAPXamSHarp
    {
        Task<string> ValidateLogin(string Username, string PasswordUser);
    }
}
