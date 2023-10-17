using MediatR;
using OAS.Application.Features.InvoiceFeatures.CreateInvoice;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice
{
    public sealed class CreateMiscellaneousSellInvoiceHandler : IRequestHandler<CreateMiscellaneousSellInvoiceRequest, CreateMiscellaneousSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        private readonly IUnitOfWork _unitOfWork;
        public CreateMiscellaneousSellInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateMiscellaneousSellInvoiceResponse> Handle(CreateMiscellaneousSellInvoiceRequest request, CancellationToken cancellationToken)
        {            
            Invoice invoice = new();
            invoice.Id = Guid.NewGuid();
            invoice.CustomerId= Guid.Parse("245db4b9-4aed-43e5-a02a-001202523e86");
            invoice.RegisterDate = DateTime.Now;
            invoice.Code = await _invoiceRepository.GetNewInvoiceCode();
            invoice.Type = Domain.Enums.InvoiceType.Sell;
            await _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new CreateMiscellaneousSellInvoiceResponse(invoice.Id, invoice.Code);
            return response;
        }
    }
}
