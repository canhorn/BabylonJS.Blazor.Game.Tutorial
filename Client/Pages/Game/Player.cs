namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using BABYLON;
    using EventHorizon.Blazor.Interop;

    [JsonConverter(typeof(CachedEntityConverter<Player>))]
    public class Player : TransformNode
    {
        private readonly Scene _scene;
        private readonly Mesh _mesh;

        public Player(
            IDictionary<string, Mesh> assets,
            Scene scene,
            ShadowGenerator shadowGenerator
        ) : base("player", scene)
        {
            _scene = scene;
            SetupPlayerCamera();

            _mesh = assets["mesh"];
            _mesh.parent = this;

            shadowGenerator.addShadowCaster(assets["mesh"]);

            // _input = input;
        }

        private void SetupPlayerCamera()
        {
            var camera4 = new ArcRotateCamera(
                "arc",
                -(decimal)System.Math.PI / 2,
                (decimal)System.Math.PI / 2,
                radius: 40,
                new Vector3(0, 3, 0),
                _scene
            );
            _scene.activeCamera = camera4;
            camera4.attachControl(true);
        }
    }
}
