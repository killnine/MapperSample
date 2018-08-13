using System;
using System.Collections.Generic;
using System.Linq;
using Model.Domain.MachineData.Press;
using Model.External.HP;

namespace Model.Mappers.MachineData.HP
{
    public interface IHpMachinePaperMapper 
    {
        MappingResult Sync(PaperData destination, HpSpecification source);
        bool ShouldBeNull(PaperData data, List<string> ignoreProperties = null);
    }

    public class HpMachinePaperMapper : MachinePaperMapper<PaperData, HpSpecification>, IHpMachinePaperMapper
    {
        public override bool ShouldBeNull(PaperData data, List<string> ignoreProperties = null)
        {
            var properties = new List<string>
            {
                nameof(data.LinearFeet),
                nameof(data.PaperPartNumber),
                nameof(data.PaperType),
                nameof(data.PaperWidth)
            };

            if(ignoreProperties != null)
            {
                properties = properties.Union(ignoreProperties).ToList();
            }

            return base.ShouldBeNull(data, properties);
        }

        internal override void LinearFeet(PaperData destination)
        {
            destination.LinearFeet = 100.0M;
        }

        internal override void PaperPartNumber(PaperData destination)
        {
            destination.PaperPartNumber = "SamplePaper";
        }

        internal override void PaperType(PaperData destination)
        {
            destination.PaperType = Types.PaperType.Sheet;
        }

        internal override void PaperWidth(PaperData destination)
        {
            destination.PaperWidth = 36.0M;
        }
    }
}
