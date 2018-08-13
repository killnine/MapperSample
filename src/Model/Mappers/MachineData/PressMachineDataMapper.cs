using Model.Press;

namespace Model.Mappers.MachineData
{
    public abstract class PressMachineDataMapper<T> : MachineDataMapper<PressMachineData, T>
    {
        internal abstract void PaperConsumption(PressMachineData data);

        public override MappingResult Sync(PressMachineData destination, T source)
        {
            SetSourceItem(source);

            PaperConsumption(destination);

            return base.Sync(destination, source);
        }
    }
}
