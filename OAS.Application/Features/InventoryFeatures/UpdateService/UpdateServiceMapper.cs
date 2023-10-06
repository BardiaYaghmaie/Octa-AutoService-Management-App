﻿using AutoMapper;
using OAS.Application.Features.InventoryFeatures.AddService;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateService
{
    public sealed class UpdateServiceMapper : Profile
    {
        public UpdateServiceMapper()
        {
            CreateMap<Service, ServiceHistory>().AfterMap((s,d) =>
            {
                d.UpdateDate = DateTime.Now;
                d.ServiceId = s.Id;
            });
        }
    }
}