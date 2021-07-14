namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using BABYLON;
    using BabylonJS.Blazor.Game.Tutorial.Client.BabylonJSExtensions;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;

    [JsonConverter(typeof(CachedEntityConverter<Player>))]
    public class Player : TransformNode
    {
        // Static/Constants
        private static readonly Vector3 ORIGINAL_TILT = new(0.5934119456780721m, 0, 0);
        private static readonly Vector3 DOWN_TILT = new Vector3(0.8290313946973066m, 0, 0);
        private static readonly decimal PLAYER_SPEED = 0.45m;
        private static readonly decimal PLAYER_OFFSET = 0.5m;
        private static readonly decimal GRAVITY = -2.8m;
        private static readonly decimal JUMP_FORCE = 0.8m;
        private static readonly decimal DASH_FACTOR = 2.5m;
        private static readonly int DASH_TIME = 10;

        private readonly Scene _scene;
        private readonly PlayerInput _input;

        // Camera
        private UniversalCamera _camera;
        private TransformNode _cameraRoot;
        private TransformNode _yTilt;

        // Animations 
        private AnimationGroup _run;
        private AnimationGroup _idle;
        private AnimationGroup _jump;
        private AnimationGroup _land;
        private AnimationGroup _dash;
        // Animation - Trackers
        private AnimationGroup _currentAnimation;
        private AnimationGroup _prevAnimation;
        private bool _isFalling;
        private bool _jumped;

        // Movement
        private decimal _deltaTime;
        private Vector3 _moveDirection = Vector3.Zero();
        private Vector3 _gravity = Vector3.Zero();
        private Vector3 _lastGroundPosition = Vector3.Zero();
        private bool _grounded;
        private decimal _horizontal;
        private decimal _vertical;
        private decimal _inputAmount;
        // Jumping
        private int _jumpCount;
        // Dashing
        private bool _dashPressed;
        private bool _canDash;
        private int _dashTime;

        // Player
        public Mesh Mesh { get; internal set; }
        // Player - Public State
        public int LanternsLit { get; internal set; } = 1;
        public bool Win { get; internal set; }
        // Sparkler
        public ParticleSystem Sparkler { get; private set; }
        public bool SparkLit { get; internal set; }
        public bool SparkReset { get; internal set; }
        // Observables
        //public Observable<CachedEntity> OnRun { get; set; } = new Observable<CachedEntity>();

        public event EventHandler<bool> OnRun;

        // sfx
        public Sound LightSfx { get; set; }
        public Sound SparkResetSfx { get; set; }
        private Sound _resetSfx;
        private Sound _walkingSfx;
        private Sound _jumpingSfx;
        private Sound _dashingSfx;

        public Player(
            GameAssets assets,
            Scene scene,
            ShadowGenerator shadowGenerator,
            PlayerInput input
        ) : base("player", scene, true)
        {
            _scene = scene;

            // Setup Sounds
            LoadSounds(scene);

            // Setup Camera
            SetupPlayerCamera();

            Mesh = assets.Mesh;
            Mesh.parent = this;

            // Anmiation Setup
            _idle = assets.AnimationGroups[1];
            _jump = assets.AnimationGroups[2];
            _land = assets.AnimationGroups[3];
            _run = assets.AnimationGroups[4];
            _dash = assets.AnimationGroups[0];

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
                        Mesh.position.copyFrom(_lastGroundPosition);
                        // -- SOUNDS --
                        _resetSfx.play();
                        return Task.CompletedTask;
                    })
                )
            );

            _scene.getLightByName("sparklight").parent = _scene.getTransformNodeByName("Empty");

            // Create the sparkler particle system
            CreateSparkles();
            SetupAnimations();
            shadowGenerator.addShadowCaster(Mesh);

            // -- SOUNDS --
            OnRun += (_, play) =>
            {
                if (play && !_walkingSfx.isPlaying)
                {
                    _walkingSfx.play();
                }
                else if (!play && _walkingSfx.isPlaying)
                {
                    _walkingSfx.stop();
                    // Make sure that walkingsfx.stop is called only once
                    _walkingSfx.isPlaying = false;
                }

            };

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

        private void SetupAnimations()
        {
            _scene.stopAllAnimations();
            _run.loopAnimation = true;
            _idle.loopAnimation = true;

            // Initiailze current and previous
            _currentAnimation = _idle;
            _prevAnimation = _land;
        }

        private void AnimatePlayer()
        {
            var isDirectionalMove = _input.InputMap["ArrowUp"]
                || _input.InputMap["ArrowDown"]
                || _input.InputMap["ArrowLeft"]
                || _input.InputMap["ArrowRight"];
            if (!_dashPressed
                && !_isFalling
                && !_jumped
                && isDirectionalMove
            )
            {
                _currentAnimation = _run;
                OnRun.Invoke(null, true);
            }
            else if (_jumped && !_isFalling && !_dashPressed)
            {
                _currentAnimation = _jump;
            }
            else if (!_isFalling && _grounded)
            {
                _currentAnimation = _idle;
                if (_scene.getSoundByName(
                    "walking"
                ).isPlaying)
                {
                    OnRun.Invoke(null, false);
                }
            }
            else if (_isFalling)
            {
                _currentAnimation = this._land;
            }

            //Animations
            if (_currentAnimation != null
                && _prevAnimation != _currentAnimation
            )
            {
                _prevAnimation.stop();
                _currentAnimation.play(_currentAnimation.loopAnimation);
                _prevAnimation = _currentAnimation;
            }
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

                // Animations
                _currentAnimation = _dash;
                _dashingSfx.play();
            }

            var dashFactor = 1.0m;
            if (_dashPressed)
            {
                if (_dashTime > DASH_TIME)
                {
                    _dashTime = 0;
                    _dashPressed = false;
                }
                else
                {
                    dashFactor = DASH_FACTOR;
                }
                _dashTime++;
            }

            // --MOVEMENTS BASED ON CAMERA--
            var foward = _cameraRoot.forward();
            var right = _cameraRoot.right();
            var correctedVertical = foward.scaleInPlace(_vertical);
            var correctedHorizontal = right.scaleInPlace(_horizontal);

            correctedHorizontal = correctedHorizontal.addInPlace(correctedVertical);
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

            _moveDirection = _moveDirection.scaleInPlace(_inputAmount * PLAYER_SPEED);

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
                Mesh.position.y + PLAYER_OFFSET,
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
            var slop1 = SlopeRaycastFrom(
                Mesh.position.x,
                Mesh.position.z + 0.25m
            );
            if (slop1.hit
                && slop1.pickedMesh.name.Contains("stair")
            )
            {
                return true;
            }
            var slop2 = SlopeRaycastFrom(
                Mesh.position.x,
                Mesh.position.z - 0.25m
            );
            if (slop2.hit
                && slop2.pickedMesh.name.Contains("stair")
            )
            {
                return true;
            }
            var slop3 = SlopeRaycastFrom(
                Mesh.position.x + 0.25m,
                Mesh.position.z
            );
            if (slop3.hit
                && slop3.pickedMesh.name.Contains("stair")
            )
            {
                return true;
            }
            var slop4 = SlopeRaycastFrom(
                Mesh.position.x - 0.25m,
                Mesh.position.z
            );
            if (slop4.hit
                && slop4.pickedMesh.name.Contains("stair")
            )
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

            // Limit the speed of gravity to the negative of the jump power
            if (_gravity.y < -JUMP_FORCE)
            {
                _gravity.y = -JUMP_FORCE;
            }

            // Cue falling animation once gravity starts pusing down
            if (_gravity.y < 0
                && _jumped
            )
            {
                _isFalling = true;
            }

            Mesh.moveWithCollisions(_moveDirection.add(_gravity));

            if (IsGrounded())
            {
                _gravity.y = 0;
                _grounded = true;
                _lastGroundPosition = _lastGroundPosition.copyFrom(Mesh.position);

                // Jumping
                _jumpCount = 1;

                // Dashing
                _canDash = true;
                _dashTime = 0;
                _dashPressed = false;

                // Jump and Falling animation flags
                _jumped = false;
                _isFalling = false;
            }

            // Jump Detected
            if (_input.JumpKeyDown
                && _jumpCount > 0)
            {
                _gravity.y = JUMP_FORCE;
                _jumpCount--;

                // Jumping and Falling animation flags
                _jumped = true;
                _isFalling = false;
                _jumpingSfx.play();
            }
        }

        private void BeforeRenderUpdate()
        {
            UpdateFromControls();
            UpdateGroundDetection();
            AnimatePlayer();
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

            var centerPlayer = Mesh.position.y + 2;
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

        private void CreateSparkles()
        {
            var sphere = Mesh.CreateSphere(
                "sparkles",
                4,
                1,
                _scene
            );
            sphere.position = new Vector3(0, 0, 0);
            // Place particle system at the tip of the sparkler on the player mesh
            sphere.parent = _scene.getTransformNodeByName(
                "Empty"
            );
            sphere.isVisible = false;

            var particleSystem = new ParticleSystem(
                "sparkles",
                1000,
                _scene
            );
            particleSystem.particleTexture = new Texture(
                _scene,
                "textures/flwr.png"
            );
            particleSystem.emitter = sphere;
            // Here we create a new proxy to the expected 'ParticleEmitterType'
            particleSystem.particleEmitterType = new IParticleEmitterTypeCachedEntity(
                new SphereParticleEmitter(0)
            );

            particleSystem.updateSpeed = 0.014m;
            particleSystem.minAngularSpeed = 0;
            particleSystem.maxAngularSpeed = 360;
            particleSystem.minEmitPower = 1;
            particleSystem.maxEmitPower = 3;

            particleSystem.minSize = 0.5m;
            particleSystem.maxSize = 2;
            particleSystem.minScaleX = 0.5m;
            particleSystem.minScaleY = 0.5m;
            particleSystem.color1 = new Color4(
                0.8m,
                0.8549019607843137m,
                1,
                1
            );
            particleSystem.color2 = new Color4(
                0.8509803921568627m,
                0.7647058823529411m,
                1,
                1
            );

            particleSystem.addRampGradient(
                0,
                Color3.White()
            );
            particleSystem.addRampGradient(
                1,
                Color3.Black()
            );
            particleSystem.getRampGradients()[0].color = Color3.FromHexString("#BBC1FF");
            particleSystem.getRampGradients()[1].color = Color3.FromHexString("#FFFFFF");
            particleSystem.maxAngularSpeed = 0;
            particleSystem.maxInitialRotation = 360;
            particleSystem.minAngularSpeed = -10;
            particleSystem.maxAngularSpeed = 10;

            particleSystem.start();

            Sparkler = particleSystem;
        }

        private void LoadSounds(
            Scene scene
        )
        {
            LightSfx = new Sound(
                "light",
                "./sounds/Rise03.mp3",
                scene
            );

            SparkResetSfx = new Sound(
                "sparkReset",
                "./sounds/Rise04.mp3",
                scene
            );

            _jumpingSfx = new Sound(
                "jumping",
                "./sounds/187024__lloydevans09__jump2.wav",
                scene
            );
            _jumpingSfx.setVolume(0.25m);

            _dashingSfx = new Sound(
                "dashing",
                "./sounds/194081__potentjello__woosh-noise-1.wav",
                scene
            );

            _walkingSfx = new Sound(
                "walking",
                "./sounds/Concrete 2.wav",
                scene
            );
            _walkingSfx.setVolume(0.2m);
            _walkingSfx.setPlaybackRate(0.6m);
            _walkingSfx.loop = true;

            _resetSfx = new Sound(
                "reset",
                "./sounds/Retro Magic Protection 25.wav",
                scene
            );
            _resetSfx.setVolume(0.25m);
        }
    }
}
