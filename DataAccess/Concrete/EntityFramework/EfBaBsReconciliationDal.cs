using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBaBsReconciliationDal : EfEntityRepositoryBase<BaBsReconciliation, ContextDb>, IBaBsReconciliationDal
    {
        public List<BaBsReconciliationDto> GetAllDto(int companyId)
        {
            using (var context = new ContextDb())
            {
                var result = from reconciliation in context.BaBsReconciliations.Where(x => x.CompanyId == companyId)
                             join company in context.Companies on reconciliation.CompanyId equals company.Id
                             join account in context.CurrencyAccounts on reconciliation.CurrencyAccountId equals account.Id
                             select new BaBsReconciliationDto
                             {
                                 CompanyId = companyId,
                                 CurrencyAccountId = account.Id,
                                 AccountIdentityNumber = account.IdentityNumber,
                                 AccountName = account.Name,
                                 AccountTaxDepartment = account.TaxDepartment,
                                 AccountTaxIdNumber = account.TaxIdNumber,
                                 CompanyIdentityNumber = company.IdentityNumber,
                                 CompanyName = company.Name,
                                 CompanyTaxDepartment = company.TaxDepartment,
                                 CompanyTaxIdDepartment = company.TaxIdNumber,
                                 Total = reconciliation.Total,
                                 EmailReadDate = reconciliation.EmailReadDate,
                                 Guid = reconciliation.Guid,
                                 Id = reconciliation.Id,
                                 IsEmailRead = reconciliation.IsEmailRead,
                                 IsResultSucceed = reconciliation.IsResultSucceed,
                                 IsSendEmail = reconciliation.IsSendEmail,
                                 ResultDate = reconciliation.ResultDate,
                                 ResultNote = reconciliation.ResultNote,
                                 SendEmailDate = reconciliation.SendEmailDate,
                                 CurrencyCode = "TL",
                                 AccountEmail = account.Email,
                                 Mounth = reconciliation.Mounth,
                                 Year = reconciliation.Year,
                                 Type = reconciliation.Type,
                                 Quantity = reconciliation.Quantity,


                             };
                return result.ToList();
            }

        }
    }
}
