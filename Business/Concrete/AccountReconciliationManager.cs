using Business.Abstract;
using Business.BusinessAspects;
using Business.Constans;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal _accountReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;

        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _accountReconciliationDal = accountReconciliationDal;
            _currencyAccountService = currencyAccountService;
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliation accountReconciliation)
        {
            string guid = Guid.NewGuid().ToString();
            _accountReconciliationDal.Add(accountReconciliation);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);


                        if (code != "Cari Kodu" && code != null)
                        {
                            DateTime startingDate = reader.GetDateTime(1);
                            DateTime endingDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double debit = reader.GetDouble(4);
                            double credit = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetByCode(code, companyId).Data.Id;
                            string guid = Guid.NewGuid().ToString();

                            AccountReconciliation accountReconciliation = new AccountReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyCredit = Convert.ToDecimal(credit),
                                CurrencyDebit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt16(currencyId),
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                Guid = guid
                            };

                            _accountReconciliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }

            File.Delete(filePath);

            return new SuccessResult(Messages.AddedAccountReconciliation);

        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Delete(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Delete(accountReconciliation);
            return new SuccessResult(Messages.DeletedAccountReconciliation);
        }

        [PerformanceAspect(2)]
        public IDataResult<AccountReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<AccountReconciliation>(_accountReconciliationDal.Get(x => x.Guid == code));
        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<AccountReconciliation> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliation>(_accountReconciliationDal.Get(x => x.Id == id));

        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliation>> GetList(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>(_accountReconciliationDal.GetList(x => x.CompanyId == companyId));
        }

        public IDataResult<List<AccountReconciliationDto>> GetListDto(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliationDto>>(_accountReconciliationDal.GetAllDto(companyId));
        }


        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.SendMail,Admin")]
        public IResult SendReconciliationMail(AccountReconciliationDto accountReconciliationDto)
        {

            string subject = "Mütakabat Maili";
            string body = $"Şirket Adımız : {accountReconciliationDto.CompanyName} <br /> " +
                $"Şirket Vergi Dairesi : {accountReconciliationDto.CompanyTaxDepartment} <br /> " +
                $"Şirket Vergi Numarası : {accountReconciliationDto.CompanyTaxIdDepartment} <br /><hr> " +

                $"Sizin Şirket : {accountReconciliationDto.AccountName} <br /> " +
                $"Sizin Şirket Vergi Dairesi : {accountReconciliationDto.AccountTaxDepartment} <br /> " +
                $"Sizin Şirket Vergi Numarası : {accountReconciliationDto.AccountTaxIdNumber} <br /><hr> " +

                $"Borç : {accountReconciliationDto.CurrencyDebit} {accountReconciliationDto.CurrencyCode} <br /> " +
                $"Borç Alacak : {accountReconciliationDto.CurrencyCredit} {accountReconciliationDto.CurrencyCode} <br /> ";


            string link = "https://localhost:7022/api/AccountReconciliations/GetByCode?code=" + accountReconciliationDto.Guid;
            string linkDescription = "Mütakabat Cevaplamak için Tıklayın";

            var mailTemplate = _mailTemplateService.GetByTemplateName("Kayıt", 6);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

            var mailParameter = _mailParameterService.Get(6);
            Entities.Dtos.SendMailDto sendMailDto = new Entities.Dtos.SendMailDto()
            {
                mailParameter = mailParameter.Data,
                email = accountReconciliationDto.AccountEmail,
                subject = "Kullanıcı Kayıt Onay Maili",
                body = templateBody
            };

            _mailService.SendMail(sendMailDto);

            return new SuccessResult(Messages.MailSendSuccessful);

        }

        [PerformanceAspect(2)]
        [SecuredOperation("AccountReconciliation.Update,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Update(accountReconciliation);
            return new SuccessResult(Messages.DeletedAccountReconciliation);
        }

        
    }
}
