using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olabilir");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz");

            
        }
    }
}
