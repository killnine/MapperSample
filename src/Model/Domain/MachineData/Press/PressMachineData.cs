using Model.Domain.MachineData.Press;

namespace Model.Press
{
    public class PressMachineData : MachineDataBase
    {
        public PaperData PaperConsumption { get; set; }
    }
}
