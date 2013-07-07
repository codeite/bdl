using System;
using System.Globalization;
using System.Linq;

namespace bdl
{
    public static class ConvertToBdlString
    {
        public static string ConvertToString(this BdlQueryComponent component)
        {
            if (component is BdlValue)
            {
                var value = component as BdlValue;
                return value.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (component is BdlBlock)
            {
                var block = component as BdlBlock;
                return "(" + block.Content.ConvertToString() + ")";
            }

            if (component is BdlAnd)
            {
                var and = component as BdlAnd;
                return string.Join("+", and.Components.Select(x => x.ConvertToString()));
            }

            if (component is BdlOr)
            {
                var or = component as BdlOr;
                return string.Join(".", or.Components.Select(x => x.ConvertToString()));
            }

            if (component is BdlNot)
            {
                var not = component as BdlNot;
                return "!" + not.Next.ConvertToString();
            }

            throw new InvalidOperationException();
        }
    }
}