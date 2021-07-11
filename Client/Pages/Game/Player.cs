namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using BABYLON;
    using BabylonJS.Blazor.Game.Tutorial.Client.BabylonJSExtensions;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;

    [JsonConverter(typeof(CachedEntityConverter<Player>))]
    public class Player : TransformNode
    {
        private static readonly Vector3 ORIGINAL_TILT = new(0.5934119456780721m, 0, 0);
        private static readonly Vector3 DOWN_TILT = new Vector3(0.8290313946973066m, 0, 0);
        private static readonly decimal PLAYER_SPEED = 0.45m;
        private static readonly decimal PLAYER_OFFSET = 0.5m;
        private static readonly decimal GRAVITY = -2.8m;
        private static readonly decimal JUMP_FORCE = 0.8m;
        private static readonly decimal DASH_FACTOR = 2.5m;
        private static readonly int DASH_TIME = 10;

        private readonly Scene _scene;
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
        private int _jumpCount;
        private bool _dashPressed;
        private bool _canDash;
        private int _dashTime;

        public Mesh Mesh { get; internal set; }
        public bool SparkLit { get; internal set; }
        public bool SparkReset { get; internal set; }
        public int LanternsLit { get; internal set; } = 1;
        public bool Win { get; internal set; }

        public Player(
            IDictionary<string, Mesh> assets,
            Scene scene,
            ShadowGenerator shadowGenerator,
            PlayerInput input
        ) : base("player", scene, true)
        {
            _scene = scene;
            SetupPlayerCamera();

            Mesh = assets["mesh"];
            Mesh.parent = this;

            // --COLLISIONS--
            Mesh.actionManager = new ActionManager(
                _scene
            );

            // Platform Destination
            Mesh.actionManager.registerAction(
                new ExecuteCodeAction(
                    new
                    {
                        trigger = ActionManager.OnIntersectionEnterTrigger,
                        parameter = _scene.getMeshByName("destination"),
                    },
                    new ActionCallback<ActionEvent>(_ =>
                    {
                        if (LanternsLit >= 22)
                        {
                            Win = true;
                            // tilt camera to look at where the fireworks will be displayed
                            _yTilt.rotation = new Vector3(
                                5.689773361501514m,
                                0.23736477827122882m,
                                0
                            );
                            _yTilt.position = new Vector3(0, 6, 0);
                            _camera.position.y = 17;
                        }
                        return Task.CompletedTask;
                    })
                )
            );
            Mesh.actionManager.registerAction(
                new ExecuteCodeAction(
                    new
                    {
                        trigger = ActionManager.OnIntersectionEnterTrigger,
                        parameter = _scene.getMeshByName("ground"),
                    },
                    new ActionCallback<ActionEvent>(_ =>
                    {
                        // need to use copy or else they will be both pointing at the same thing & update together
                        Mesh.position.copyFrom(this._lastGroundPosition);
                        return Task.CompletedTask;
                    })
                )
            );

            _scene.getLightByName("sparklight").parent = _scene.getTransformNodeByName("Empty");

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

            // Dashing
            if (_input.Dashing
                && !_dashPressed
                && _canDash
                && !_grounded
            )
            {
                // We have started a dash, do not allow another
                _canDash = false;
                // Start the dash sequence
                _dashPressed = true;
            }

            var dashFactor = 1.0m;
            if (_dashPressed)
            {
                dashFactor = DASH_FACTOR;
                if (_dashTime > DASH_TIME)
                {
                    _dashTime = 0;
                    _dashPressed = false;
                }
                _dashTime++;
            }

            // --MOVEMENTS BASED ON CAMERA--
            var foward = _cameraRoot.forward();
            var right = _cameraRoot.right();
            var correctedVertical = foward.scale(_vertical);
            var correctedHorizontal = right.scale(_horizontal);

            correctedHorizontal = correctedHorizontal.add(correctedVertical);
            var move = correctedHorizontal;

            _moveDirection = new Vector3(move.normalize().x * dashFactor, 0, move.normalize().z * dashFactor);

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
            Mesh.rotationQuaternion = Quaternion.Slerp(
                Mesh.rotationQuaternion,
                targ,
                10 * _deltaTime
            );
        }

        private Vector3 FloorRaycast(decimal offsetX, decimal offsetZ, decimal raycastLength)
        {
            var raycastFloorPosition = new Vector3(
                Mesh.position.x + offsetX,
                Mesh.position.y - PLAYER_OFFSET,
                Mesh.position.z + offsetZ
            );
            var ray = new Ray(
                raycastFloorPosition,
                Vector3.Up().scale(-1),
                raycastLength
            );

            var pick = _scene.pickWithRay(
                ray
            );

            if (pick.hit)
            {
                return pick.pickedPoint;
            }

            return Vector3.Zero();
        }

        private bool IsGrounded()
        {
            var floorRaycast = FloorRaycast(
                0,
                0,
                0.6m
            );
            if (Vector3AreEqual(
                floorRaycast,
                Vector3.Zero()
            ))
            {
                return false;
            }

            return true;
        }

        private bool CheckSlope()
        {
            if (SlopeRaycastFrom(
                Mesh.position.x,
                Mesh.position.z + 0.25m
            ).hit)
            {
                return true;
            }
            if (SlopeRaycastFrom(
                Mesh.position.x,
                Mesh.position.z - 0.25m
            ).hit)
            {
                return true;
            }
            if (SlopeRaycastFrom(
                Mesh.position.x + 0.25m,
                Mesh.position.z
            ).hit)
            {
                return true;
            }
            if (SlopeRaycastFrom(
                Mesh.position.x - 0.25m,
                Mesh.position.z
            ).hit)
            {
                return true;
            }

            return false;
        }

        private static bool Vector3AreEqual(
            Vector3 first,
            Vector3 second
        )
        {

            return first.x == second.x
                && first.y == second.y
                && first.z == second.z;
        }

        private PickingInfo SlopeRaycastFrom(
            decimal x,
            decimal z
        )
        {
            var raycast = new Vector3(
                x,
                Mesh.position.y + PLAYER_OFFSET,
                z
            );
            var ray = new Ray(
                raycast,
                Vector3.Up().scale(-1),
                1.5m
            );
            return _scene.pickWithRay(
                ray
            );
        }

        private void UpdateGroundDetection()
        {
            if (!IsGrounded())
            {
                if (CheckSlope()
                    && _gravity.y <= 0)
                {
                    _gravity.y = 0;
                    _jumpCount = 1;
                    _grounded = true;
                }
                else
                {
                    _gravity = _gravity.addInPlace(
                        Vector3.Up().scale(
                            _deltaTime * GRAVITY
                        )
                    );
                    _grounded = false;
                }
            }

            if (_gravity.y < -JUMP_FORCE)
            {
                _gravity.y = -JUMP_FORCE;
            }
            Mesh.moveWithCollisions(_moveDirection.add(_gravity));

            if (IsGrounded())
            {
                _gravity.y = 0;
                _grounded = true;
                _lastGroundPosition.copyFrom(Mesh.position);

                // Jumping
                _jumpCount = 1;

                // Dashing
                _canDash = true;
                _dashTime = 0;
                _dashPressed = false;
            }

            // Jump Detected
            if (_input.JumpKeyDown
                && _jumpCount > 0)
            {
                _gravity.y = JUMP_FORCE;
                _jumpCount--;
            }
        }

        private void BeforeRenderUpdate()
        {
            UpdateFromControls();
            UpdateGroundDetection();
        }

        private void UpdateCamera()
        {
            if (Mesh.intersectsMesh(
                _scene.getMeshByName("cornerTrigger")
            ))
            {
                if (_input.HorizontalAxis > 0)
                {
                    _cameraRoot.rotation = Vector3.Lerp(
                        _cameraRoot.rotation,
                        new Vector3(
                            _cameraRoot.rotation.x,
                            (decimal)Math.PI / 2,
                            _cameraRoot.rotation.z
                        ),
                        0.4m
                    );
                }
                else if (_input.HorizontalAxis < 0)
                {
                    _cameraRoot.rotation = Vector3.Lerp(
                        _cameraRoot.rotation,
                        new Vector3(
                            _cameraRoot.rotation.x,
                            (decimal)Math.PI,
                            _cameraRoot.rotation.z
                        ),
                        0.4m
                    );
                }
            }
            //rotates the camera to point down at the player when they enter the area, and returns it back to normal when they exit
            else if (Mesh.intersectsMesh(
                _scene.getMeshByName("festivalTrigger")
            ))
            {
                if (_input.VerticalAxis > 0)
                {
                    _yTilt.rotation = Vector3.Lerp(
                        _yTilt.rotation,
                        DOWN_TILT,
                        0.4m
                    );
                }
                else if (_input.VerticalAxis < 0)
                {
                    _yTilt.rotation = Vector3.Lerp(
                        _yTilt.rotation,
                        ORIGINAL_TILT,
                        0.4m
                    );
                }
            }
            //once you've reached the destination area, return back to the original orientation, if they leave rotate it to the previous orientation
            else if (Mesh.intersectsMesh(
                _scene.getMeshByName("destinationTrigger")
            ))
            {
                if (_input.VerticalAxis > 0)
                {
                    _yTilt.rotation = Vector3.Lerp(
                        _yTilt.rotation,
                        ORIGINAL_TILT,
                        0.4m
                    );
                }
                else if (_input.VerticalAxis < 0)
                {
                    _yTilt.rotation = Vector3.Lerp(
                        _yTilt.rotation, 
                        DOWN_TILT, 
                        0.4m
                    );
                }
            }

            var centerPlayer = Mesh.position.y + PLAYER_OFFSET;
            _cameraRoot.position = Vector3.Lerp(
                _cameraRoot.position,
                new Vector3(
                    Mesh.position.x,
                    centerPlayer,
                    Mesh.position.z
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
