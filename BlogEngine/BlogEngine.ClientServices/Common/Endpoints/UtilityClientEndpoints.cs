namespace BlogEngine.ClientServices.Common.Endpoints
{
    public sealed class UtilityClientEndpoints
    {
        public static string BlogContentPDFConverterFullAddress(int id) => $"https://localhost:44328/api/utilities/pdf/blog/{id}";
    }
}