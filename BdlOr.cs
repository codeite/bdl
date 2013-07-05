using System.Collections.Generic;
using System.Linq;

namespace bdl
{
    public class BdlOr : BdlQueryComponent
    {
        private readonly List<BdlQueryComponent> _components;

        public BdlOr(List<BdlQueryComponent> components)
        {
            _components = components;
        }

        public bool Evaluate(params int[] values)
        {
            return _components.Any(component => component.Evaluate(values));
        }

        public string ConvertToString()
        {
            return string.Join(".", _components.Select(x => x.ConvertToString()));
        }

        public string ConvertToXQuery(string varName)
        {
            return string.Join(" or ", _components.Select(x => x.ConvertToXQuery(varName)));
        }
    }
}