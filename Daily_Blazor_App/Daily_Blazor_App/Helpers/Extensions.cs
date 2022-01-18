using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Daily_Blazor_App.Helpers
{
    public static class Extensions
    {
        public static float ToDpi(this float centimeter)
        {
            var inch = centimeter / 2.54;
            return (float)(inch * 72);
        }

        public async static Task SetDefaultCulture()
        {
            //var js = (IJSInProcessRuntime)JSRuntime;
            //var result = await js.InvokeAsync<string>("blazorCulture.get");
            //CultureInfo culture;
            //if (result != null)
            //    culture = new CultureInfo(result);
            //else
            //    culture = new CultureInfo("en-US");
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
