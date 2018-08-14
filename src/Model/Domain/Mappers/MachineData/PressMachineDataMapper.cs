using Model.Domain.MachineData.Press;

namespace Model.Domain.Mappers.MachineData
{
    public abstract class PressMachineDataMapper<T> : MachineDataMapper<PressMachineData, T>
    {
        protected abstract void PaperConsumption(PressMachineData data);

        public override MapResult Map(PressMachineData destination, T source)
        {
            SetSourceItem(source);

            PaperConsumption(destination);

            return base.Map(destination, source);
        }
    }
}
