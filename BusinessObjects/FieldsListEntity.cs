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
using System.Collections.Specialized;
using System.Text;

namespace BusinessObjects
{    
    public class FieldsListEntity
    {        
        public List<FieldsEntity> FieldsList { get; set; }

        public Dictionary<string, object> PdfExtractFieldPageList { get; set; }        

        public int PageNo { get; set; }        
    }   
}
