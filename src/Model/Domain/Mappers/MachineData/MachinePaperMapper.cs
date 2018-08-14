using Model.Domain.MachineData.Press;

namespace Model.Domain.Mappers.MachineData
{
    public abstract class MachinePaperMapper<T,TV> : MapperBase<T, TV> where T : PaperData
    {
        protected abstract void PaperWidth(T destination);
        protected abstract void LinearFeet(T destination);
        protected abstract void PaperPartNumber(T destination);
        protected abstract void PaperType(T destination);

        public override MapResult Map(T destination, TV source)
        {
            SetSourceItem(source);

            PaperType(destination);
            PaperPartNumber(destination);
            LinearFeet(destination);
            PaperWidth(destination);

            return base.Map(destination, source);
        }
    }
}
