#region Copyright

// =============================================================================================================================================
//  Copyright (c) 2018 <Company Name>
//  ALL RIGHTS RESERVED.
//  <Address>

//
//  Unauthorized distribution, adaptation or use may be subject to civil and criminal penalties.
// =============================================================================================================================================

#endregion

#region Header

// Author				: OCRFormProcessing
// Created date			: 01 Jan 2018
// Description			: 
// History
// =========================================================================================================================
// Date			BugID		Description														Developer       Reviewed By
// =========================================================================================================================
// 
// =========================================================================================================================

#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ApproveEligibleEntity : CommonEntity
    {
        public Guid ClaimId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StructuredJson { get; set; }

        public string Status { get; set; }

        public DateTime CoverageEffectiveDate { get; set; }

        public Int64 ErrorId { get; set; }        

        public bool Eligible { get; set; }

        public bool Approve { get; set; }

        public string DependentName { get; set; }

        public string AccidentDescription { get; set; }

        public bool WorkRelated { get; set; }

        public DateTime DateOfAccident { get; set; }

        public string Diagnosis { get; set; }

        public DateTime DateOfService { get; set; }

        public string BenifitName { get; set; }

        public int PageNo { get; set; }

        public string Rationale { get; set; }
    }
}
