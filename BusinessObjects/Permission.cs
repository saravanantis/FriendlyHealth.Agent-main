using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class Permission
    {
        public string DisplayName { get; private set; }

        public static readonly Permission DocumentUpload = GetPermission("ENABLE DOCUMENT UPLOAD");
        public static readonly Permission DocumentProcessing = GetPermission("ENABLE DOCUMENT PROCESSING");
        public static readonly Permission DocumentDelete = GetPermission("ENABLE DOCUMENT DELETE");
        public static readonly Permission ResultDownload = GetPermission("ENABLE RESULT DOWNLOAD");
        public static readonly Permission ResultPreview = GetPermission("ENABLE RESULT PREVIEW");
        public static readonly Permission ResultAdjudicate = GetPermission("ENABLE RESULT ADJUDICATE");
        public static readonly Permission ResultSubmission = GetPermission("ENABLE RESULT SUBMISSION");
        public static readonly Permission ConfigManagement = GetPermission("ENABLE CONFIG MANAGEMENT");
        public static readonly Permission UserManagement = GetPermission("ENABLE USER MANAGEMENT");
        public static readonly Permission SharedDocumentAccess = GetPermission("ENABLE SHARED DOCUMENT ACCESS");
        public static readonly Permission ModifyUserSettings = GetPermission("ENABLE MODIFY USER SETTINGS");
        public static readonly Permission ViewAllDocument = GetPermission("ENABLE VIEW ALL USERS DOCUMENTS");
        public static readonly Permission POSTMESSAGE = GetPermission("ENABLE DOCUMENT POST MESSAGE");
        public static readonly Permission APPLICATIONTYPEMANAGEMENT = GetPermission("ENABLE APPLICATION TYPE MANAGEMENT");

        private Permission(string permissionName)
        {
            this.DisplayName = permissionName;
        }

        public static Permission GetPermission(string permissionName)
        {
            switch (permissionName)
            {
                case "ENABLE DOCUMENT UPLOAD": return new Permission(permissionName);
                case "ENABLE DOCUMENT PROCESSING": return new Permission(permissionName);
                case "ENABLE DOCUMENT DELETE": return new Permission(permissionName);
                case "ENABLE RESULT DOWNLOAD": return new Permission(permissionName);
                case "ENABLE RESULT PREVIEW": return new Permission(permissionName);
                case "ENABLE RESULT ADJUDICATE": return new Permission(permissionName);
                case "ENABLE RESULT SUBMISSION": return new Permission(permissionName);
                case "ENABLE CONFIG MANAGEMENT": return new Permission(permissionName);
                case "ENABLE USER MANAGEMENT": return new Permission(permissionName);
                case "ENABLE SHARED DOCUMENT ACCESS": return new Permission(permissionName);
                case "ENABLE MODIFY USER SETTINGS": return new Permission(permissionName);
                case "ENABLE VIEW ALL USERS DOCUMENTS": return new Permission(permissionName);
                case "ENABLE DOCUMENT POST MESSAGE": return new Permission(permissionName);
                case "ENABLE APPLICATION TYPE MANAGEMENT": return new Permission(permissionName);
                default:
                    throw new Exception("Invalid permission type");
            }
        }
    }
}
