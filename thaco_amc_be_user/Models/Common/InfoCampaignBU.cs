namespace UserManagement.Models.Common
{
    public class InfoCampaignBU
    {
        public string campaign_id { get; set; }
        public string campaign_name { get; set; }
        public int total_campaigntask_running { get; set; }
        public int total_campaigntask_complete { get; set; }
        public int total_campaigntask_progress { get; set; }
    }
}
