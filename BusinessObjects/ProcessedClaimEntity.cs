using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ProcessedClaimEntity : CommonEntity
    {
        public List<ClaimEntity> ClaimEntities { get; set; } = new List<ClaimEntity>();
    }
}
