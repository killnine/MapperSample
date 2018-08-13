using System;
using System.Linq;
using Model.Domain.MachineData.Press;
using Model.External.HP;
using Model.Mappers.MachineData;
using Model.Mappers.MachineData.HP;
using Model.Press;

namespace Model.Mappers.HP
{
    public class HpMachineDataMapper : PressMachineDataMapper<HpSpecification>
    {
        private readonly IHpMachinePaperMapper _paperMapper;

        internal override void PaperConsumption(PressMachineData data)
        {
            if(!SourceItem.submission.Any())
            {
                return;
            }

            data.PaperConsumption = new PaperData();
            var mappingResult = _paperMapper.Sync(data.PaperConsumption, SourceItem);
            if(_paperMapper.ShouldBeNull(data.PaperConsumption))
            {
                data.PaperConsumption = null;
            }
        }

        internal override void JobNumber(MachineDataBase data)
        {
            data.JobNumber = SourceItem.customerjobid;
        }

        internal override void GrossCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.GrossCount = SourceItem.submission[0].statistics[0].copiesprintederror + SourceItem.submission[0].statistics[0].copiesprintedok;
        }

        internal override void NetCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.NetCount = SourceItem.submission[0].statistics[0].copiesprintedok;
        }

        internal override void WasteCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.WasteCount = SourceItem.submission[0].statistics[0].copiesprintederror;
        }

        internal override void Unit(MachineDataBase data)
        {
            data.Unit = Types.CountUnitType.Copies;
        }

        public override MappingResult Sync(PressMachineData destination, HpSpecification source)
        {
            return base.Sync(destination, source);
        }

        public HpMachineDataMapper(IHpMachinePaperMapper paperMapper)
        {
            _paperMapper = paperMapper;
        }
    }
}
