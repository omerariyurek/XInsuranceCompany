using System;
using System.Collections.Generic;
using System.Text;

namespace XInsuranceCompany.Core.Entities.Common
{
    public interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
    }
}
