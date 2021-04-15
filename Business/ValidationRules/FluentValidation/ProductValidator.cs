using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
   public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(r => r.ProductName).NotEmpty().WithMessage("Ürün Adı Boş Olamaz");
            RuleFor(r => r.ProductName).MinimumLength(2);
            RuleFor(r => r.UnitPrice).NotEmpty();
            RuleFor(r => r.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartsWithA);
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
