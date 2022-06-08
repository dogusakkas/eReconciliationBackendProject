using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CurrencyAccountValidator : AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş geçilemez");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("İsim en az 3 karakter olabilir");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Şirket numarası boş geçilemez");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş geçilemez");
        }
    }
}
