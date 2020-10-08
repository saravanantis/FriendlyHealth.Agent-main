using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class CompanyClaim : ErrorLogEntity
    {
        public string CompanyId { get; set; }
        public List<ContractClaim> ContractClaims { get; set; }
    }
}
