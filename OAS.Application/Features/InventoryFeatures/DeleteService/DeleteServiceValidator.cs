using FluentValidation;
using OAS.Application.Features.InventoryFeatures.DeleteInventoryItem;
using OAS.Application.Features.InventoryFeatures.DeleteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddService
{
    public class DeleteServiceValidator : AbstractValidator<DeleteServiceRequest>
    {
        public DeleteServiceValidator()
        {
            RuleFor(x => x.Code)
          .NotNull()
          .GreaterThan(0);
        }
    }
}
