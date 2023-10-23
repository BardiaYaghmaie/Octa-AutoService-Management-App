using FluentValidation;
using OAS.Application.Features.CustomerFeatures.AddCustomer;
using OAS.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.AddnvoicePayment
{
    public class AddInvoicePaymentValidator : AbstractValidator<AddInvoicePaymentRequest>
    {
        public AddInvoicePaymentValidator()
        {
            RuleFor(request => request.TrackCodeAndAmountList)
    .Must(TrackCodeAndAmountListIsValid)
    .WithMessage("All amounts in the TrackCodeAndAmountList must be non-negative.");

        }
        private bool TrackCodeAndAmountListIsValid(List<Tuple<string, long>> trackCodeAndAmountList)
        {
            foreach (var tuple in trackCodeAndAmountList)
            {
                if (tuple.Item2 < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
