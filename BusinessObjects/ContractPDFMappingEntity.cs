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
    public class ContractPDFMappingEntity : CommonEntity
    {
        public Int64 ContractPDFMappingId { get; set; }

        public Guid ContractId { get; set; }

        public string PDFImage { get; set; }

        public int PageNumber { get; set; }

        public bool IsDeleted { get; set; }

        public string JSONWordList { get; set; }

        public string JSONParaList { get; set; }

        public string ExtractFieldBoundaryList { get; set; }
    }
}
