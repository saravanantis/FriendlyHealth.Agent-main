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
    public class CommonEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Message { get; set; }

        public bool StatusCode { get; set; }

        
    }
}
