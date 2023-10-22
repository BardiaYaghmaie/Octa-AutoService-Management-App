using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesHandler : IRequestHandler<GetDailySellInvoicesRequest, GetDailySellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetDailySellInvoicesHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetDailySellInvoicesResponse> Handle(GetDailySellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetDailySellInvoicesAsync();
            var data1 = data.Where(a => a.VehicleId.HasValue || a.CustomerId.HasValue)
            .Select((a, i) => new GetDailySellInvoices_DTO(
                a.Id,
                a.Code.ToString(),
                a.VehicleId.HasValue ? a.Vehicle.Name : "",
                a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName) : a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : "",
                i + 1

            )).ToList();
            var response = new GetDailySellInvoicesResponse(data1);
            return response;
        }
    }
}
