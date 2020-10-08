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

using Amazon;

namespace BusinessObjects
{
    public class AppSettings
    {
        public AppSettings(string connection)
        {
            StorageConnectionString = connection;
        }

        public AppSettings(string connection, string accessKey, string secretKey, string bucketName, RegionEndpoint region)
        {
            StorageConnectionString = connection;
            AccessKey = accessKey;
            SecretKey = secretKey;
            BucketName = bucketName;
            Region = region;
        }
        public string StorageConnectionString { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string BucketName { get; set; }

        public RegionEndpoint Region { get; set; }

    }
}
