using Daily_Blazor_App.Data;
using Parse;

namespace Daily_Blazor_App.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);

            ParseClient client = new ParseClient(Daily_Blazor_App_Logic.Consts.APP_ID, Daily_Blazor_App_Logic.Consts.APP_URI, Daily_Blazor_App_Logic.Consts.NETKEY);                                    
        }
    }
}
