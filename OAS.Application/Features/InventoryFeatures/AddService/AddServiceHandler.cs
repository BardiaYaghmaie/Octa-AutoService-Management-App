﻿using AutoMapper;
using MediatR;
using OAS.Application.Features.InventoryFeatures.AddInventoryItem;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddService
{
    public class AddServiceHandler : IRequestHandler<AddServiceRequest, AddServiceResponse>
    {
        private readonly IServiceRepository  _serviceRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddServiceHandler(IServiceRepository serviceRepository, IServiceHistoryRepository serviceHistoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



    
        public async Task<AddServiceResponse> Handle(AddServiceRequest request, CancellationToken cancellationToken)
        {
            var serviceToAdd = _mapper.Map<Service>(request);
            int code = await _serviceRepository.GetNewCodeAsync();
            serviceToAdd.Code = code;
            await _serviceRepository.AddAsync(serviceToAdd);
            var serviceHistoryToAdd = _mapper.Map<ServiceHistory>(request);
            serviceHistoryToAdd.ServiceId= serviceToAdd.Id;
            await _serviceHistoryRepository.AddAsync(serviceHistoryToAdd);
            var response = new AddServiceResponse(serviceToAdd.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return response;
        }
    }
}