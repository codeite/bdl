namespace bdl
{
    public class BdlBlock : BdlQueryComponent
    {
        private readonly BdlQueryComponent _content;

        public BdlBlock(BdlQueryComponent content)
        {
            _content = content;
        }

        public bool Evaluate(params int[] value)
        {
            return _content.Evaluate(value);
        }

        public string ConvertToString()
        {
            return "(" + _content.ConvertToString() + ")";
        }

        public string ConvertToXQuery(string varName)
        {
            return "(" + _content.ConvertToXQuery(varName) + ")";
        }
    }
}