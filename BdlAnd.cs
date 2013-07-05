using System.Collections.Generic;
using System.Linq;

namespace bdl
{
    public class BdlAnd : BdlQueryComponent
    {
        private readonly List<BdlQueryComponent> _components;

        public BdlAnd(List<BdlQueryComponent> components)
        {
            _components = components;
        }

        public bool Evaluate(params int[] values)
        {
            return _components.All(component => component.Evaluate(values));
        }

        public string ConvertToString()
        {
            return string.Join("+", _components.Select(x => x.ConvertToString()));
        }

        public string ConvertToXQuery(string varName)
        {
            return string.Join(" and ", _components.Select(x => x.ConvertToXQuery(varName)));
        }
    }
}