namespace UnityApiPoc.Values
{
    public class ValuesProvider : IValuesProvider
    {
        public string GetValues()
        {
            return "Got values using DI!";
        }
    }
}