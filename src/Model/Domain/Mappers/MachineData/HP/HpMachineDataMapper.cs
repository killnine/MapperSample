using System;
using System.Collections.Generic;
using System.Linq;
using Model.DataTransfer.External.HP;
using Model.Domain.MachineData;
using Model.Domain.MachineData.Press;

namespace Model.Domain.Mappers.MachineData.HP
{
    public interface IHpMachineDataMapper
    {
        MapResult Map(PressMachineData destination, HpSpecification source);
        bool ShouldBeNull(PressMachineData obj, List<string> ignoreProperties = null);
    }

    public class HpMachineDataMapper : PressMachineDataMapper<HpSpecification>, IHpMachineDataMapper
    {
        private readonly IHpMachinePaperMapper _paperMapper;

        protected override void PaperConsumption(PressMachineData data)
        {
            if(!SourceItem.submission.Any())
            {
                return;
            }

            try
            {
                data.PaperConsumption = new PaperData();
                var mappingResult = _paperMapper.Map(data.PaperConsumption, SourceItem);
                if (_paperMapper.ShouldBeNull(data.PaperConsumption))
                {
                    data.PaperConsumption = null;
                }
            }
            catch (Exception ex)
            {
                AddError("Unable to map PaperConsumption data: " + ex.Message);
            }
        }

        protected override void JobNumber(MachineDataBase data)
        {
            data.JobNumber = SourceItem.customerjobid;
        }

        protected override void GrossCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.GrossCount = SourceItem.submission[0].statistics[0].copiesprintederror + SourceItem.submission[0].statistics[0].copiesprintedok;
        }

        protected override void NetCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.NetCount = SourceItem.submission[0].statistics[0].copiesprintedok;
        }

        protected override void WasteCount(MachineDataBase data)
        {
            if (!SourceItem.submission.Any() || !SourceItem.submission[0].statistics.Any())
            {
                return;
            }

            data.WasteCount = SourceItem.submission[0].statistics[0].copiesprintederror;
        }

        protected override void Unit(MachineDataBase data)
        {
            data.Unit = Types.CountUnitType.Copies;
        }

        public HpMachineDataMapper(IHpMachinePaperMapper paperMapper)
        {
            _paperMapper = paperMapper;
        }
    }
}
