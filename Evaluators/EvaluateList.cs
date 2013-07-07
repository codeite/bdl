using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bdl
{
    public static class EvaluateList
    {
        public static bool Evaluate(this BdlQueryComponent component, params int[] values)
        {
            if (component is BdlValue)
            {
                var value = component as BdlValue;
                return values.Contains(value.Value);
            }

            if (component is BdlBlock)
            {
                var block = component as BdlBlock;
                return block.Content.Evaluate(values);
            }

            if (component is BdlAnd)
            {
                var and = component as BdlAnd;
                return and.Components.All(component1 => component1.Evaluate(values));
            }

            if (component is BdlOr)
            {
                var or = component as BdlOr;
                return or.Components.Any(component1 => component1.Evaluate(values));
            }

            if (component is BdlNot)
            {
                var not = component as BdlNot;
                return !not.Next.Evaluate(values);
            }

            throw new InvalidOperationException();
        }
    }
}
