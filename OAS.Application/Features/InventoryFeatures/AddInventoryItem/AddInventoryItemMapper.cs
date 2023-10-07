using AutoMapper;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddInventoryItem
{
    public sealed class AddInventoryItemMapper : Profile
    {
        public AddInventoryItemMapper()
        {
            CreateMap<AddInventoryItemRequest, InventoryItem>().AfterMap((s,d) =>
            {
                d.Id = Guid.NewGuid();
                d.IsActive = true;
                d.RegisterDate = DateTime.Now;
            });
            CreateMap<AddInventoryItemRequest, InventoryItemHistory>();
        }
    }
}
