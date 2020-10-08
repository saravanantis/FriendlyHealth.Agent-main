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
    public class MedicalAnnotationsEntity : CommonEntity
    {
        public Int64 MedicalAnnotationsId { get; set; }

        public Guid ClaimId { get; set; }

        public string Note { get; set; }

        public int PageNumber { get; set; }

        public Int64 CreatedBy { get; set; }
    }
}
