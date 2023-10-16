using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed class GetInvoiceReportInfoHandler : IRequestHandler<GetInvoiceReportInfoRequest, GetInvoiceReportInfoResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceReportInfoHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetInvoiceReportInfoResponse> Handle(GetInvoiceReportInfoRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetInvoiceReportInfoAsync(request.InvoiceId);
            return data;
        }
    }
}
