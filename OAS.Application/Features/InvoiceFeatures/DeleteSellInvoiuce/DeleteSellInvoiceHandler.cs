using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.DeleteSellInvoiuce
{
    public sealed class DeleteSellInvoiceHandler : IRequestHandler<DeleteSellInvoiceRequest, DeleteSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSellInvoiceHandler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<DeleteSellInvoiceResponse> Handle(DeleteSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.Id);
            if (invoice == null)
                throw new Exception("invoice not found");
            _invoiceRepository.Delete(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new DeleteSellInvoiceResponse(request.Id);
        }
    }
}
