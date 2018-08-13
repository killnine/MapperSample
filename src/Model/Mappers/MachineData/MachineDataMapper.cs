namespace Model.Mappers.MachineData
{
    public abstract class MachineDataMapper<T,TV> : MapperBase<T, TV> where T : MachineDataBase
    {
        internal abstract void JobNumber(MachineDataBase data);
        internal abstract void GrossCount(MachineDataBase data);
        internal abstract void NetCount(MachineDataBase data);
        internal abstract void WasteCount(MachineDataBase data);
        internal abstract void Unit(MachineDataBase data);

        public override MappingResult Sync(T destination, TV source)
        {
            SetSourceItem(source);

            JobNumber(destination);
            GrossCount(destination);
            NetCount(destination);
            WasteCount(destination);
            Unit(destination);

            return base.Sync(destination, source);
        }
    }
}