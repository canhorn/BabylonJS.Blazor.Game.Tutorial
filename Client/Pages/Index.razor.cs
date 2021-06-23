namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages
{
    using System;
    using System.Threading.Tasks;
    using BABYLON;
    using BabylonJS.Blazor.Game.Tutorial.Client.BabylonJSExtensions;
    using BabylonJS.Blazor.Game.Tutorial.Client.HTML;
    using EventHorizon.Blazor.Interop.Callbacks;
    using Microsoft.AspNetCore.Components.Web;

    public partial class Index : IDisposable
    {
        private Engine _engine;
        private DebugLayerScene _scene;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                CreateScene();
            }
        }

        public void Dispose()
        {
            _engine?.dispose();
        }

        public void CreateScene()
        {
            var canvas = Canvas.GetElementById(
                "game-window"
            );
            var engine = new Engine(
                canvas,
                true
            );
            // We extend the standard Scene with the DebugLayer getter in the DebugLayerScene
            _scene = new DebugLayerScene(
                engine
            );
            var light1 = new HemisphericLight(
                "light1",
                new Vector3(
                    0,
                    2,
                    8
                ),
                _scene
            );
            var camera = new ArcRotateCamera(
                "Camera",
                (decimal)(Math.PI / 2),
                (decimal)(Math.PI / 4),
                2,
                Vector3.Zero(),
                _scene
            );
            _scene.activeCamera = camera;
            camera.attachControl(
                false
            );
            var sphere = MeshBuilder.CreateSphere(
                "sphere",
                new
                {
                    diameter = 1
                },
                _scene
            );

            engine.runRenderLoop(new ActionCallback(
                () => Task.Run(() => _scene.render(true, false))
            ));

            _engine = engine;
        }


        protected void HandleKeyDown(
            KeyboardEventArgs args
        )
        {
            Console.WriteLine(args.Key);
            if (args.ShiftKey && args.CtrlKey && args.AltKey && args.Key.ToLower() == "i")
            {
                if (_scene.debugLayer.isVisible())
                {
                    Console.WriteLine("Hello");
                    _scene.debugLayer.hide();
                }
                else
                {
                    _scene.debugLayer.show();
                }
            }
        }
    }
}
