using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Şirket adı boş olamaz");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Şirket adı en az 3 karakter olmalıdır");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Şirket adresi boş geçilemez");
        }
    }
}
