using VMS.TPS.Common.Model.Types;

namespace DvhSummary.Script
{
    public class DvhSummaryViewModel
    {
        public string StructureId { get; set; }
        public double Volume { get; set; }
        public DoseValue Mean { get; set; }
        public DoseValue Min { get; set; }
        public DoseValue Max { get; set; }
        public DoseValue NearMin { get; set; }
        public DoseValue NearMax { get; set; }
    }
}