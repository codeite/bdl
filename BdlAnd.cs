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

        public List<BdlQueryComponent> Components
        {
            get { return _components; }
        }
    }
}