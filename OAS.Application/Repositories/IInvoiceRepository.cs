﻿using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice entity);
        Task<int> GetNewInvoiceCode();
    }
}
