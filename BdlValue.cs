using System.Globalization;
using System.Linq;

namespace bdl
{
    public class BdlValue : BdlQueryComponent
    {
        private readonly int _value;

        public BdlValue(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }
}