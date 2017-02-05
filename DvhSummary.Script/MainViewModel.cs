using System.Collections.Generic;
using VMS.TPS.Common.Model.API;

namespace DvhSummary.Script
{
    public class MainViewModel
    {
        public PlanSetup PlanSetup { get; set; }

        public IEnumerable<DvhSummaryViewModel> DvhSummaries { get; set; }

        public void CalculateDvhSummary()
        {
            var calculator = new DvhSummariesCalculator();
            DvhSummaries = calculator.Calculate(PlanSetup);
        }
    }
}