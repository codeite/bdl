namespace bdl
{
    public class BdlNot : BdlQueryComponent
    {
        private readonly BdlQueryComponent _next;

        public BdlNot(BdlQueryComponent next)
        {
            _next = next;
        }

        public bool Evaluate(params int[] values)
        {
            return !_next.Evaluate(values);
        }

        public string ConvertToString()
        {
            return "!" + _next.ConvertToString();
        }

        public string ConvertToXQuery(string varName)
        {
            throw new System.NotImplementedException();
        }
    }
}