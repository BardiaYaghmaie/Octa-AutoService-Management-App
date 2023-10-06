﻿using AutoMapper;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.DeleteInventoryItem
{
    public sealed class DeleteInventoryItemMapper : Profile
    {
        public DeleteInventoryItemMapper()
        {
            CreateMap<InventoryItem, InventoryItemHistory>().AfterMap((s, d) =>
            {
                d.UpdateDate= DateTime.Now;
                d.InventoryItemId = s.Id;
            });
        }
    }
}
