using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.AddSellInvoicePayment
{
    public sealed class AddInvoiceHandler : IRequestHandler<AddInvoiceRequest, AddInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddInvoiceResponse> Handle(AddInvoiceRequest request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null)
                throw new Exception("");

            List<InvoicePayment> invoicePayments = request.TrackCodeAndAmountList.Select(a => new InvoicePayment
            {
                InvoiceId = invoice.Id,
                Id = Guid.NewGuid(),
                TrackCode = a.Item1,
                PaidAmount = a.Item2,
                LastPaymentDate = DateTime.UtcNow
            }).ToList();

            await _invoiceRepository.AddInvoicePaymentsAsync(invoicePayments);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new AddInvoiceResponse(invoice.Id);
            return response;

        }
    }
}
