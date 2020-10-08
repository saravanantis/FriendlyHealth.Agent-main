using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class ContractClaim
    {
        public string ContractNumber { get; set; }
        public List<ClaimEntity> Claims { get; set; }
        public List<Fields> Fields { get; set; }
    }
}
