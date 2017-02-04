using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace DvhSummary.Script
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            DvhSummaries = new List<DvhSummaryViewModel>();
        }

        public PlanSetup PlanSetup { get; set; }

        public List<DvhSummaryViewModel> DvhSummaries { get; }

        public void CalculateDvhSummary()
        {
            var structures = PlanSetup?.StructureSet?.Structures?.Where(s => !s.IsEmpty);

            if (structures == null)
            {
                return;
            }

            foreach (var structure in structures)
            {
                var dvh = GetDvh(structure);

                var dvhSummary = new DvhSummaryViewModel
                {
                    StructureId = structure.Id,
                    Volume = structure.Volume,
                    Mean = dvh.MeanDose,
                    Min = dvh.MinDose,
                    Max = dvh.MaxDose,
                    NearMin = GetNearMin(structure),
                    NearMax = GetNearMax(structure)
                };

                DvhSummaries.Add(dvhSummary);
            }
        }

        private DVHData GetDvh(Structure structure)
        {
            return PlanSetup.GetDVHCumulativeData(structure,
                DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.01);
        }

        public DoseValue GetNearMin(Structure structure)
        {
            return PlanSetup.GetDoseAtVolume(structure, 98.0,
                VolumePresentation.Relative, DoseValuePresentation.Absolute);
        }

        public DoseValue GetNearMax(Structure structure)
        {
            return PlanSetup.GetDoseAtVolume(structure, 0.1,
                VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
        }
    }
}