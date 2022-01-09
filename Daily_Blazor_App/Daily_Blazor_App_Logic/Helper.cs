using Parse;

namespace Daily_Blazor_App_Logic
{
    public static class Helper
    {
        public static ParseClient GetParseClient()
        {
            ParseClient client = new ParseClient(Daily_Blazor_App_Logic.Consts.APP_ID, Daily_Blazor_App_Logic.Consts.APP_URI, Daily_Blazor_App_Logic.Consts.NETKEY);
            return client;
        }
    }
}
