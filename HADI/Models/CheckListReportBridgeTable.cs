namespace HADI.Models
{
    public class CheckListReportBridgeTable
    {
        public int CheckListId { get; set; }
        public CheckList CheckList { get; set; }

        public int CheckListReportId { get; set; }
        public CheckListReport CheckListReport { get; set; }
    }
}
