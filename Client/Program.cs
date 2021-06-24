namespace BabylonJS.Blazor.Game.Tutorial.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BlazorPro.BlazorSize;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Setup Blazor Services
            builder.Services.AddScoped<ResizeListener>();

            await builder.Build().RunAsync();
        }
    }
}
