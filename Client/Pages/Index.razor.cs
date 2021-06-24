namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game;
    using BlazorPro.BlazorSize;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;

    public partial class Index : IDisposable
    {
        [Inject]
        public ResizeListener ResizeListener { get; set; } = null!;


        private GameApp _app;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _app = new GameApp("game-window");
                await _app.Main();
                ResizeListener.OnResized += HandleWindowResize;
            }
        }

        private void HandleWindowResize(object sender, BrowserWindowSize e)
        {
            _app?.Resize();
        }

        public void Dispose()
        {
            _app?.Dispose();
        }

        protected async Task HandleKeyDown(
            KeyboardEventArgs args
        )
        {
            if (args.ShiftKey && args.CtrlKey && args.AltKey && args.Key.ToLower() == "i")
            {
                if (_app.DebugLayer.isVisible())
                {
                    _app.DebugLayer.hide();
                }
                else
                {
                    await _app.DebugLayer.show();
                }
            }
        }
    }
}
