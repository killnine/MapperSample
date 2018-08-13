using Model.Domain.MachineData;

namespace Model.Mappers.MachineData
{
    public abstract class MachineDataMapper<T,TV> : MapperBase<T, TV> where T : MachineDataBase
    {
        protected abstract void JobNumber(MachineDataBase data);
        protected abstract void GrossCount(MachineDataBase data);
        protected abstract void NetCount(MachineDataBase data);
        protected abstract void WasteCount(MachineDataBase data);
        protected abstract void Unit(MachineDataBase data);

        public override MapResult Map(T destination, TV source)
        {
            SetSourceItem(source);

            JobNumber(destination);
            GrossCount(destination);
            NetCount(destination);
            WasteCount(destination);
            Unit(destination);

            return base.Map(destination, source);
        }
    }
}