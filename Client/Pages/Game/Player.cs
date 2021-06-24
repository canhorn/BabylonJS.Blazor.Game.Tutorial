namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using BABYLON;
    using EventHorizon.Blazor.Interop;

    [JsonConverter(typeof(CachedEntityConverter<Player>))]
    public class Player : TransformNode
    {
        private static readonly Vector3 ORIGINAL_TILT = new(0.5934119456780721m, 0, 0);

        private readonly Scene _scene;
        private readonly Mesh _mesh;
        private TransformNode _cameraRoot;
        private TransformNode _yTilt;
        private UniversalCamera _camera;

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

        private void _updateCamera()
        {
            var centerPlayer = _mesh.position.y + 2;
            _cameraRoot.position = Vector3.Lerp(
                _cameraRoot.position,
                new Vector3(
                    _mesh.position.x,
                    centerPlayer,
                    _mesh.position.z
                ),
                0.4m
            );
        }

        private void SetupPlayerCamera()
        {
            var cameraRoot = new TransformNode("root");
            _cameraRoot = cameraRoot;
            cameraRoot.position = Vector3.Zero();
            cameraRoot.rotation = new Vector3(0, (decimal)System.Math.PI, 0);

            var yTilt = new TransformNode("y-tilt");
            yTilt.rotation = ORIGINAL_TILT;
            _yTilt = yTilt;
            yTilt.parent = _cameraRoot;

            var camera = new UniversalCamera(
                "cam",
                new Vector3(0, 0, -30),
                _scene
            );
            _camera = camera;
            camera.lockedTarget = cameraRoot.position;
            camera.fov = 0.47m;
            camera.parent = yTilt;

            _scene.activeCamera = camera;
            camera.attachControl(true);
        }
    }
}
