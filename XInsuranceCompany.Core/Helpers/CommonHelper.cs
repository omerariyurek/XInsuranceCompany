using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XInsuranceCompany.Core.Helpers
{
   public class CommonHelper: ICommonHelper
    {
        public bool VerifyTcIdentificationNumber(string tcIdentityNo)
        {
            var isValid = false;
            if (!string.IsNullOrEmpty(tcIdentityNo) && tcIdentityNo.Length == 11)
            {
                try
                {
                    var isParsed = long.TryParse(tcIdentityNo, out var tcNo);
                    if (isParsed)
                    {
                        var aTcNo = tcNo / 100;
                        var bTcNo = tcNo / 100;
                        var c1 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c2 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c3 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c4 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c5 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c6 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c7 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c8 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var c9 = aTcNo % 10; aTcNo = aTcNo / 10;
                        var q1 = ((10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10);
                        var q2 = ((10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10);
                        isValid = ((bTcNo * 100) + (q1 * 10) + q2 == tcNo);
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return isValid;
        }
    }
}
