﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.DomainModels
{
    public sealed record ServiceDTO(int RowNumber, string Code, string Title, long Price);
}