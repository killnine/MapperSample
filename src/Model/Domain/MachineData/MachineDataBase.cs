using Model.Domain.Types;

namespace Model.Domain.MachineData
{
    public class MachineDataBase
    {
        public string JobNumber { get; set; }
        public int GrossCount { get; set; }
        public int NetCount { get; set; }
        public int WasteCount { get; set; }
        public CountUnitType Unit { get; set; }
    }
}
