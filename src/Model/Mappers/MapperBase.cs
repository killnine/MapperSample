using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Utilities;

namespace Model.Mappers
{
    public abstract class MapperBase<T,TV>
    {
        protected TV SourceItem { get; private set; }

        readonly IList<string> _warnings = new List<string>();
        readonly IList<string> _errors = new List<string>();

        protected void AddWarning(string message) 
        {
            _warnings.Add(message);    
        }

        protected void AddError(string message)
        {
            _errors.Add(message);
        }

        protected virtual void SetSourceItem(TV sourceItem)
        {
            SourceItem = sourceItem;    
        }

        public virtual MapResult Map(T destination, TV source)
        {
            SetSourceItem(source);

            var result = new MapResult()
            {
                Warnings = _warnings,
                Errors = _errors,
                Success = !_errors.Any()
            };

            _warnings.Clear();
            _errors.Clear();

            return result;
        }

        public virtual bool ShouldBeNull(T obj, List<string> ignoreProperties = null)
        {
            if (obj == null)
                return true;

            var propertiesToIgnore = ignoreProperties ?? new List<string>();

            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var noPropertiesSet = properties.Where(s => !s.Name.In(propertiesToIgnore.ToArray())).Select(x =>
            {
                /* Check for an empty List first. If found, set to null as default is null. */
                var listVal = x.GetValue(obj, null) as IEnumerable<object>;
                if (listVal?.Count() == 0) return null;

                var val = x.GetValue(obj);
                return val;
            }).All(y =>
            {
                if (y is IEnumerable<object> asList)
                {
                    return !asList.Any();
                }
                var val = GetDefault(y?.GetType());
                return y?.ToString() == val?.ToString();
            });
            return noPropertiesSet;
        }

        private static object GetDefault(Type type)
        {
            if (type != null && type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
