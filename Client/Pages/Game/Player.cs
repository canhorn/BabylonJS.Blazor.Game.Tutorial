namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using BABYLON;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;

    [JsonConverter(typeof(CachedEntityConverter<Player>))]
    public class Player : TransformNode
    {
        private static readonly Vector3 ORIGINAL_TILT = new(0.5934119456780721m, 0, 0);
        private static readonly decimal PLAYER_SPEED = 0.45m;
        private static readonly decimal GRAVITY = -2.8m;
        private static readonly decimal JUMP_FORCE = 0.8m;

        private readonly Scene _scene;
        private readonly Mesh _mesh;
        private TransformNode _cameraRoot;
        private TransformNode _yTilt;
        private UniversalCamera _camera;
        private PlayerInput _input;

        private decimal _deltaTime;
        private Vector3 _moveDirection = Vector3.Zero();
        private Vector3 _gravity = Vector3.Zero();
        private Vector3 _lastGroundPosition = Vector3.Zero();
        private bool _grounded;
        private decimal _horizontal;
        private decimal _vertical;
        private decimal _inputAmount;

        public Player(
            IDictionary<string, Mesh> assets,
            Scene scene,
            ShadowGenerator shadowGenerator,
            PlayerInput input
        ) : base("player", scene, true)
        {
            _scene = scene;
            SetupPlayerCamera();

            _mesh = assets["mesh"];
            _mesh.parent = this;

            shadowGenerator.addShadowCaster(assets["mesh"]);

            _input = input;
        }

        public UniversalCamera ActivatePlayerCamera()
        {
            _scene.registerBeforeRender(new ActionCallback(
                () =>
                {
                    BeforeRenderUpdate();
                    UpdateCamera();

                    return Task.CompletedTask;
                }
            ));

            return _camera;
        }

        private void UpdateFromControls()
        {
            _deltaTime = _scene.getEngine().getDeltaTime() / 1000.0m;

            _moveDirection = Vector3.Zero();
            _horizontal = _input.Horizontal;
            _vertical = _input.Vertical;

            // --MOVEMENTS BASED ON CAMERA--
            var foward = _cameraRoot.forward;
            var right = _cameraRoot.right;
            var correctedVertical = foward.scale(_vertical);
            var correctedHorizontal = right.scale(_horizontal);

            correctedHorizontal = correctedHorizontal.add(correctedVertical);
            var move = correctedHorizontal;

            _moveDirection = new Vector3(move.normalize().x, 0, move.normalize().z);

            var inputMag = Math.Abs(_horizontal) + Math.Abs(_vertical);
            if (inputMag < 0)
            {
                _inputAmount = 0;
            }
            else if (inputMag > 1)
            {
                _inputAmount = 1;
            }
            else
            {
                _inputAmount = inputMag;
            }

            _moveDirection = _moveDirection.scale(_inputAmount * PLAYER_SPEED);

            // Rotations
            var input = new Vector3(_input.HorizontalAxis, 0, _input.VerticalAxis);
            if (input.length() == 0)
            {
                return;
            }
            var angle = Math.Atan2((double)_input.HorizontalAxis, (double)_input.VerticalAxis);
            angle += (double)_cameraRoot.rotation.y;
            var targ = Quaternion.FromEulerAngles(0, (decimal)angle, 0);
            _mesh.rotationQuaternion = Quaternion.Slerp(_mesh.rotationQuaternion, targ, 10 * _deltaTime);
        }

        private Vector3 FloorRaycast(decimal offsetX, decimal offsetZ, decimal raycastLength)
        {
            var raycastFloorPosition = new Vector3(
                _mesh.position.x + offsetX,
                _mesh.position.y + 0.5m,
                _mesh.position.z + offsetZ
            );
            var ray = new Ray(
                raycastFloorPosition,
                Vector3.Up().scale(-1),
                raycastLength
            );

            var pick = _scene.pickWithRay(
                ray,
                new ActionCallback<AbstractMesh>(
                    mesh => Task.FromResult(
                        mesh.isPickable && mesh.isEnabled()
                    )
                )
            );
            if (pick.hit)
            {
                return pick.pickedPoint;
            }

            return Vector3.Zero();
        }

        private bool IsGrounded()
        {
            var floorRaycast = FloorRaycast(0, 0, 0.6m);
            if (floorRaycast.y != 0)
            {
                return false;
            }

            return true;
        }

        private void UpdateGroundDetection()
        {
            if (!IsGrounded())
            {
                _gravity = _gravity.addInPlace(Vector3.Up().scale(_deltaTime * GRAVITY));
                _grounded = false;
            }

            if (_gravity.y < -JUMP_FORCE)
            {
                _gravity.y = 0;
            }
            _mesh.moveWithCollisions(_moveDirection.add(_gravity));

            if (IsGrounded())
            {
                _gravity.y = 0;
                _grounded = true;
                _lastGroundPosition.copyFrom(_mesh.position);
            }
        }

        private void BeforeRenderUpdate()
        {
            UpdateFromControls();
            UpdateGroundDetection();
        }

        private void UpdateCamera()
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
            var cameraRoot = new TransformNode(
                "root",
                _scene,
                true
            );
            _cameraRoot = cameraRoot;
            cameraRoot.position = new Vector3(0, 0, 0);
            cameraRoot.rotation = new Vector3(0, (decimal)Math.PI, 0);

            var yTilt = new TransformNode(
                "ytilt",
                _scene,
                true
            );
            _yTilt = yTilt;
            yTilt.rotation = ORIGINAL_TILT;
            yTilt.parent = _cameraRoot;

            var camera = new UniversalCamera(
                "cam",
                new Vector3(0, 0, -30),
                _scene
            );
            _camera = camera;
            camera.lockedTarget = cameraRoot.position;
            camera.fov = 0.47m;
            camera.parent = _yTilt;

            _scene.activeCamera = camera;
        }
    }
}
