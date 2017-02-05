using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace DvhSummary.Script
{
    public class DvhSummariesCalculator
    {
        public IEnumerable<DvhSummaryViewModel> Calculate(PlanSetup plan)
        {
            var structures = plan?.StructureSet?.Structures?.Where(s => !s.IsEmpty);

            if (structures == null)
            {
                return null;
            }

            var dvhSummaries = new List<DvhSummaryViewModel>();

            foreach (var structure in structures)
            {
                var dvh = GetDvh(plan, structure);

                var dvhSummary = new DvhSummaryViewModel
                {
                    StructureId = structure.Id,
                    Volume = structure.Volume,
                    Mean = dvh.MeanDose,
                    Min = dvh.MinDose,
                    Max = dvh.MaxDose,
                    NearMin = GetNearMin(plan, structure),
                    NearMax = GetNearMax(plan, structure)
                };

                dvhSummaries.Add(dvhSummary);
            }

            return dvhSummaries;
        }

        private DVHData GetDvh(PlanSetup plan, Structure structure)
        {
            return plan.GetDVHCumulativeData(structure,
                DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.01);
        }

        private DoseValue GetNearMin(PlanSetup plan, Structure structure)
        {
            return plan.GetDoseAtVolume(structure, 98.0,
                VolumePresentation.Relative, DoseValuePresentation.Absolute);
        }

        private DoseValue GetNearMax(PlanSetup plan, Structure structure)
        {
            return plan.GetDoseAtVolume(structure, 0.1,
                VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
        }
    }
}