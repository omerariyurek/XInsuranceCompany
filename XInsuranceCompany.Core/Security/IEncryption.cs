using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XInsuranceCompany.Core.Security
{
    public interface IEncryption
    {
        string EncryptText(string text, string privateKey = "");
        string DecryptText(string text, string privateKey = "");
    }
}
