
namespace ppsha.Models
{
    public class DocumentRequestModel
    {
        public string[] ClaimIds { get; set; }
        public bool ProcessAll { get; set; }
        public bool Debug { get; set; }
    }
    public class ZoomLevelModel
    {
        public float ZoomLevel { get; set; }
    }

    public class ClaimRequestModel
    {
        public string[] ClaimIds { get; set; }
        public bool ProcessAll { get; set; }
        public bool Debug { get; set; }


    }
    public class SingleClaimRequestModel
    {
        public int pageno { get; set; }
        public string claimid { get; set; }
        public bool Debug { get; set; }
    }
    public class ReviewStatustModel
    {
        public int pageno { get; set; }
        public string claimid { get; set; }
        public bool Debug { get; set; }
        public string status { get; set; }
    }
}
