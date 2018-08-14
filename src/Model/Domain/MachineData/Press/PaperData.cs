using Model.Domain.Types;

namespace Model.Domain.MachineData.Press
{
    public class PaperData
    {
        public PaperType PaperType { get; set; }
        public string PaperPartNumber { get; set; }
        public decimal LinearFeet { get; set; }
        public decimal PaperWidth { get; set; }
    }
}
