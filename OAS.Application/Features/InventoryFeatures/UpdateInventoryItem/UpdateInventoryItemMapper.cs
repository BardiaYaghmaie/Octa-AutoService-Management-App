using AutoMapper;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public sealed class UpdateInventoryItemMapper : Profile
    {
        public UpdateInventoryItemMapper()
        {
            CreateMap<UpdateInventoryItemRequest, InventoryItem>();
            CreateMap<InventoryItem, InventoryItemHistory>().AfterMap((s, d) =>
            {
                d.Id = Guid.NewGuid();
                d.UpdateDate = DateTime.UtcNow;
                d.InventoryItemId = s.Id;
            });
        }
    }
}
