using FluentValidation;
using OAS.Application.Features.InventoryFeatures.DeleteInventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddService
{
    public class DeleteInventoryItemValidator : AbstractValidator<DeleteInventoryItemRequest>
    {
        public DeleteInventoryItemValidator()
        {
            RuleFor(x => x.Code)
          .NotNull()
          .GreaterThan(0);
        }
    }
}
