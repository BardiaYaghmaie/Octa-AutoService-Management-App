using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OAS.Application.DomainModels
{
    public class InvoiceDescriptionModel
    {
        public string CurrentMileage { get; set; } = "";
        public string MileageOfTheNextEngineOilService { get; set; } = "";
        public string MileageOfTheNextGearboxOilService { get; set; } = "";
        public string MileageOfTheNextSteeringOilService { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
