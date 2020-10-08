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
    public class ContractEntity : CommonEntity
    {
        public Guid ContractId { get; set; }

        public int EmployerId { get; set; }

        public string EmployerClass { get; set; }

        public DateTime CoverageEffectiveDate { get; set; }

        public bool IsAccidentsCovered { get; set; }

        public bool IsDependentsCovered { get; set; }

        public byte[] ContractPdf { get; set; }

        public string StructuredJson { get; set; }

        public int TotalPages { get; set; }

        public int PDFId { get; set; }

        public string PDFName { get; set; }

        public bool IsDeleted { get; set; }

        public string EmployerName { get; set; }

    }
}
