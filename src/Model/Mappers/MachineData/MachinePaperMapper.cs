using System;
using System.Collections.Generic;
using Model.Domain.MachineData.Press;

namespace Model.Mappers.MachineData
{
    public abstract class MachinePaperMapper<T,TV> : MapperBase<T, TV> where T : PaperData
    {
        internal abstract void PaperWidth(T destination);
        internal abstract void LinearFeet(T destination);
        internal abstract void PaperPartNumber(T destination);
        internal abstract void PaperType(T destination);

        public override MappingResult Sync(T destination, TV source)
        {
            SetSourceItem(source);

            PaperType(destination);
            PaperPartNumber(destination);
            LinearFeet(destination);
            PaperWidth(destination);

            return base.Sync(destination, source);
        }
    }
}
