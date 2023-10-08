using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetSellInvoicesHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetSellInvoicesAsync();
            var response = new GetSellInvoicesResponse(Data: data);
            return response;
        }
    }
}
