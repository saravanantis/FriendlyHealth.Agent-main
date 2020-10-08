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

namespace BusinessObjects
{
    public class ViewClaimDetails
    {
        public Guid ClaimId { get; set; }
        public string StructuredJson { get; set; }
        public string S3FileName { get; set; }
        public string S3Url { get; set; }
        public string ExtractFieldBoundaryList { get; set; }
        public string documentStatus { get; set; }
        public bool isLocked { get; set; }
    }
}
