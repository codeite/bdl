namespace bdl
{
    public class BdlBlock : BdlQueryComponent
    {
        private readonly BdlQueryComponent _content;

        public BdlBlock(BdlQueryComponent content)
        {
            _content = content;
        }

        public BdlQueryComponent Content
        {
            get { return _content; }
        }
    }
}