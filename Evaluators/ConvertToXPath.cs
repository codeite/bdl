using System;
using System.Linq;

namespace bdl
{
    public static class ConvertToXPath
    {
        public static string ConvertToXQuery(this BdlQueryComponent component, string name)
        {
            if (component is BdlValue)
            {
                var value = component as BdlValue;
                return name + " = " + value.Value;
            }

            if (component is BdlBlock)
            {
                var block = component as BdlBlock;
                return "(" + block.Content.ConvertToXQuery(name) + ")";
            }

            if (component is BdlAnd)
            {
                var and = component as BdlAnd;
                return string.Join(" and ", and.Components.Select(x => x.ConvertToXQuery(name)));
            }

            if (component is BdlOr)
            {
                var or = component as BdlOr;
                return string.Join(" or ", or.Components.Select(x => x.ConvertToXQuery(name)));
            }

            if (component is BdlNot)
            {
                var not = component as BdlNot;
                return "not(" + not.Next.ConvertToXQuery(name) + ")";
            }

            throw new InvalidOperationException();
        }
    }
}