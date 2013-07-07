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

        public List<BdlQueryComponent> Components
        {
            get { return _components; }
        }
    }
}