using AutoMapper;
using OAS.Application.Features.InventoryFeatures.AddInventoryItem;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddService
{
    public sealed class AddServiceMapper: Profile
    {
        public AddServiceMapper()
        {
            CreateMap<AddServiceRequest, Service>().AfterMap((s,d) =>
            {
                d.Id = Guid.NewGuid();
            });
            CreateMap<AddServiceRequest, ServiceHistory>();
        }
    }
}
