namespace bdl
{
    public interface BdlQueryComponent
    {
        bool Evaluate(params int[] values);
        string ConvertToString();
        string ConvertToXQuery(string varName);
    }
}