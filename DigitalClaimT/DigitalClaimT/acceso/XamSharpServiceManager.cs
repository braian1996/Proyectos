using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalClaimT.acceso
{
    public class XamSharpServiceManager
    {
        ISOAPXamSHarp soapService;
        public XamSharpServiceManager(ISOAPXamSHarp service)
        {
            soapService = service;
        }
        Task<string> ValidateLogin(string Username, string PasswordUser)
        {
            try
            {
                return soapService.ValidateLogin(Username, PasswordUser);
            }
            catch(Exception exMsj)
            {
                throw exMsj;
            }
        }
    }
}
