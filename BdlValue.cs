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

        public bool Evaluate(params int[] values)
        {
            return values.Contains(_value);
        }

        public string ConvertToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public string ConvertToXQuery(string varName)
        {
            return varName + " = " + _value;
        }
    }
}