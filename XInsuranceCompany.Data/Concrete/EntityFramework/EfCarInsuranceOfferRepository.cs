using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using XInsuranceCompany.Core.DataAccess.EntityFramework;
using XInsuranceCompany.Core.Entities.Insurance;
using XInsuranceCompany.Data.Abstract;
using XInsuranceCompany.Data.Contexts;

namespace XInsuranceCompany.Data.Concrete.EntityFramework
{
    public class EfCarInsuranceOfferRepository : EfEntityRepositoryBase<CarInsuranceOffer>, ICarInsuranceOfferRepository
    {
        public EfCarInsuranceOfferRepository(XInsuranceCompanyDbContext context) : base(context)
        {
        }
    }
}
