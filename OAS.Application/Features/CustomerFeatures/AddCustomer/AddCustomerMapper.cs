using AutoMapper;
using OAS.Application.DomainModels;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.AddCustomer
{
    public class AddCustomerMapper:Profile
    {
        public AddCustomerMapper()
        {
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<AddCustomerRequest, Customer>().AfterMap((s, d) =>
            {
                d.Id = Guid.NewGuid();
                d.IsActive = true;
            });
        }
    }
}
