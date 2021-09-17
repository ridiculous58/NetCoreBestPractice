using Bp.Api.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api.Validations
{
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id 0 dan Kucuk veya eşit Olamaz");
        }
    }
}
