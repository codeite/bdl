namespace bdl
{
    public class BdlNot : BdlQueryComponent
    {
        private readonly BdlQueryComponent _next;

        public BdlNot(BdlQueryComponent next)
        {
            _next = next;
        }

        public BdlQueryComponent Next
        {
            get { return _next; }
        }
    }
}