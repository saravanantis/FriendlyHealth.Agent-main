using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class UserEntity : CommonEntity
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public string TokenString { get; set; }
    }

    public class UserEntityData
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Permissions { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public bool Disabled { get; set; }
        public string ClientName { get; set; }
        public string[] ClaimType { get; set; }
    }

    public class UserEntityList
    {
        public List<UserEntityData> GetUserDataList { get; set; }
    }

    public class UserPermissions : CommonEntity
    {
        public int Id { get; set; }
        public string PermissionLevel { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Enabled { get; set; }
        public int UserId { get; set; }
        public string Permissions { get; set; }
        public string UserEmail { get; set; }
    }
    public class UserPermissionsList
    {
        public List<UserPermissions> userPermissionsList { get; set; }
    }

    public class UserSettings
    {
        public int Id { get; set; }
        public string DownloadFormat { get; set; }
        public bool Shareuploadedfiles { get; set; }
    }
}
