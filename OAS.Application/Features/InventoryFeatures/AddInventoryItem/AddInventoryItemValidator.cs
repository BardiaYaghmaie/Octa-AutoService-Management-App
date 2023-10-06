using FluentValidation;
using OAS.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddInventoryItem
{
    public class AddInventoryItemValidator : AbstractValidator<AddInventoryItemRequest>
    {
        public AddInventoryItemValidator()
        {
            RuleFor(x => x.Name).
                NotEmpty().
                MaximumLength(255);
            RuleFor(x => x.SellPrice)
                .NotNull()
            .GreaterThan(0);
            RuleFor(x => x.BuyPrice)
       .NotNull()
   .GreaterThan(0);

            RuleFor(x => x.CountLowerBound)
       .NotNull()
   .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count)
.NotNull()
.GreaterThanOrEqualTo(0);

        }
    }
}
