using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ClaimTypeEntity : CommonEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRequired { get; set; }
    }
    public class ClaimTypeListEntity
    {
        public List<ClaimTypeEntity> GetClaimDataList { get; set; }
    }
    public class UserAlowedClaimtype
    {
        public int Id { get; set; }

        public int Userid { get; set; }

        public int Claimtypeid { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }


}
