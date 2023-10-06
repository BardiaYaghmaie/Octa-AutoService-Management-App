using FluentValidation;
using OAS.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateService
{
    public class UpdateServiceValidator : AbstractValidator<AddServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.Name).
                NotEmpty().
                MaximumLength(255);
            RuleFor(x => x.DefaultPrice)
                .NotNull()
            .GreaterThan(0);
        }
    }
}
