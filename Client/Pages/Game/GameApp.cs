namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BABYLON;
    using BABYLON.GUI;
    using BabylonJS.Blazor.Game.Tutorial.Client.HTML;
    using EventHorizon.Blazor.Interop.Callbacks;
    using Scene = BabylonJSExtensions.DebugLayerScene;

    public class GameApp : IDisposable
    {
        // General Entire Application
        private readonly Canvas _canvas;
        private readonly Engine _engine;

        // Scene - related
        private Scene _scene;
        private GameState _state = GameState.Start;
        private Scene _gameScene;
        private Scene _cutScene;
        private GameEnvironment _environment;
        private IDictionary<string, Mesh> _assets;
        private Player _player;
        private PlayerInput _input;

        public DebugLayer DebugLayer => _scene.debugLayer;

        public GameApp(
            string canvasId
        )
        {
            _canvas = CreateCanvas(
                canvasId
            );

            // Initialize Babylon Scene and Engine
            _engine = new Engine(
                _canvas,
                antialias: true
            );
            _scene = new Scene(
                _engine
            );

            var camera = new ArcRotateCamera(
                name: "Camera",
                (decimal)(Math.PI / 2),
                (decimal)(Math.PI / 2),
                radius: 2,
                Vector3.Zero(),
                _scene
            );
            _scene.activeCamera = camera;
            camera.attachControl(true);
        }

        public async Task Main()
        {
            await GoToStart();

            _engine.runRenderLoop(new ActionCallback(
                () =>
                {
                    switch (_state)
                    {
                        case GameState.Start:
                            return Task.Run(() => _scene.render(true, false));
                        case GameState.Game:
                            return Task.Run(() => _scene.render(true, false));
                        case GameState.Lose:
                            return Task.Run(() => _scene.render(true, false));
                        case GameState.CutScene:
                            return Task.Run(() => _scene.render(true, false));
                        default:
                            break;
                    }
                    return Task.CompletedTask;
                }
            ));
        }

        public void Resize()
        {
            _engine.resize();
        }

        public void Dispose()
        {
            _engine.dispose();
        }

        private static Canvas CreateCanvas(
            string canvasId
        )
        {
            return Canvas.GetElementById(
                canvasId
            );
        }

        private async Task SetupGame()
        {
            var scene = new Scene(
                _engine
            );
            _gameScene = scene;

            // Load Assets
            var environment = new GameEnvironment(scene);
            _environment = environment;
            await _environment.Load();

            // Load Character Assets
            _assets = await LoadCharacterAssets(scene);
        }

        private Task<IDictionary<string, Mesh>> LoadCharacterAssets(
            Scene scene
        )
        {
            var outer = MeshBuilder.CreateBox(
                "outer",
                new
                {
                    width = 2,
                    depth = 1,
                    height = 3,
                },
                scene
            );
            outer.isVisible = false;
            outer.isPickable = false;
            outer.checkCollisions = true;

            // Move the origin of the box collider to the bottom of the mesh, this will match the imported player mesh
            outer.bakeTransformIntoVertices(
                Matrix.Translation(0, 1.5m, 0)
            );

            outer.ellipsoid = new Vector3(1, 1.5m, 1);
            outer.ellipsoidOffset = new Vector3(0, 1.5m, 0);

            // Rotate player 180 to have back to camera
            outer.rotationQuaternion = new Quaternion(0, 1, 0, 0);

            var box = MeshBuilder.CreateBox(
                "Small1",
                new
                {
                    width = 0.5,
                    depth = 0.5,
                    height = 0.25,
                    faceColors = new[]
                    {
                        new Color4(0, 0, 0, 1),
                        new Color4(0, 0, 0, 1),
                        new Color4(0, 0, 0, 1),
                        new Color4(0, 0, 0, 1),
                        new Color4(0, 0, 0, 1),
                        new Color4(0, 0, 0, 1),
                    }
                },
                scene
            );
            box.position.y = 1.5m;
            box.position.z = 1;

            var body = Mesh.CreateCylinder(
                "body",
                3,
                2,
                2,
                0,
                0,
                scene
            );
            var bodyMaterial = new StandardMaterial(
                "red",
                scene
            );
            bodyMaterial.diffuseColor = new Color3(0.8m, 0.5m, 0.5m);
            body.material = bodyMaterial;
            body.isPickable = false;
            body.bakeTransformIntoVertices(Matrix.Translation(0, 1.5m, 0));

            box.parent = body;
            body.parent = outer;

            var meshMap = new Dictionary<string, Mesh>
            {
                ["mesh"] = outer,
            };

            return Task.FromResult<IDictionary<string, Mesh>>(meshMap);
        }

        #region Go To Start
        public async Task GoToStart()
        {
            _engine.displayLoadingUI();

            _scene.detachControl();
            var scene = new Scene(
                _engine
            )
            {
                clearColor = new Color4(
                    0,
                    0,
                    0,
                    1
                )
            };
            var camera = new FreeCamera(
                "camera1",
                new Vector3(
                    0,
                    0,
                    0
                ),
                scene
            );
            scene.activeCamera = camera;
            camera.setTarget(Vector3.Zero());
            camera.attachControl(true);

            var guiMenu = AdvancedDynamicTexture.CreateFullscreenUI(
                name: "UI",
                scene: scene
            );
            guiMenu.idealHeight = 720;

            var startButton = Button.CreateSimpleButton(
                "start",
                "PLAY"
            );
            startButton.width = "0.2";
            startButton.height = "40px";
            startButton.color = "white";
            startButton.top = "-14px";
            startButton.thickness = 0;
            startButton.verticalAlignment = Control.VERTICAL_ALIGNMENT_BOTTOM;
            guiMenu.addControl(startButton);

            startButton.onPointerDownObservable.add(async (_, __) =>
            {
                await GoToCutScene();
                scene.detachControl();
            });

            await scene.whenReadyAsync();
            _engine.hideLoadingUI();

            _scene.dispose();
            _scene = scene;
            _state = GameState.Start;
        }
        #endregion

        private async Task GoToCutScene()
        {
            _engine.displayLoadingUI();

            // Setup Scene
            _scene.detachControl();
            _cutScene = new Scene(
                _engine
            );
            var camera = new FreeCamera(
                "camera1",
                position: new Vector3(
                    0,
                    0,
                    0
                ),
                _cutScene
            );
            _cutScene.activeCamera = camera;
            camera.setTarget(Vector3.Zero());
            camera.attachControl(true);

            // GUI
            var cutScene = AdvancedDynamicTexture.CreateFullscreenUI(
                name: "cutscene",
                scene: _cutScene
            );

            //--PROGRESS DIALOGUE--
            var next = Button.CreateSimpleButton(
                "next",
                "NEXT"
            );
            next.color = "white";
            next.thickness = 0;
            next.verticalAlignment = Control.VERTICAL_ALIGNMENT_BOTTOM;
            next.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_RIGHT;
            next.width = "64px";
            next.height = "64px";
            next.top = "-3%";
            next.left = "-12%";
            cutScene.addControl(next);

            next.onPointerUpObservable.add(async (_, __) =>
            {
                await GoToGame();
            });

            // Scene Finished Loading
            await _cutScene.whenReadyAsync();
            _scene.dispose();
            _state = GameState.CutScene;
            _scene = _cutScene;

            _engine.hideLoadingUI();

            // Start Loading and Setup the Game
            await SetupGame();
        }

        private async Task GoToGame()
        {
            _scene.detachControl();
            var scene = _gameScene;

            // a color that fit the overall color scheme better
            scene.clearColor = new Color4(
                0.01568627450980392m,
                0.01568627450980392m,
                0.20392156862745098m,
                1
            );

            // --GUI--
            var playerUI = AdvancedDynamicTexture.CreateFullscreenUI(
                name: "UI",
                scene: scene
            );
            playerUI.idealHeight = 720;
            //dont detect any inputs from this ui while the game is loading
            scene.detachControl();

            //create a simple button
            var loseBtn = Button.CreateSimpleButton(
                "lose",
                "LOSE"
            );
            loseBtn.width = "0.2";
            loseBtn.height = "40px";
            loseBtn.color = "white";
            loseBtn.top = "-14px";
            loseBtn.thickness = 0;
            loseBtn.verticalAlignment = Control.VERTICAL_ALIGNMENT_BOTTOM;
            playerUI.addControl(loseBtn);

            //this handles interactions with the start button attached to the scene
            loseBtn.onPointerUpObservable.add(async (_, __) =>
            {
                await GoToLose();
                scene.detachControl(); //observables disabled
            });


            // --INPUT--
            _input = new PlayerInput(scene);

            // Primitive Character and Settings
            await InitializeGame(scene);

            await scene.whenReadyAsync();
            scene.getMeshByName(
                "outer"
            ).position = new Vector3(0, 3, 0);

            //get rid of start scene, switch to gamescene and change states
            _engine.hideLoadingUI();
            _scene.dispose();
            _state = GameState.Game;
            _scene = scene;

            //the game is ready, attach control back
            scene.attachControl(
                true,
                true,
                true
            );
            // We use this to fix the ActionManager
            _canvas.ResetControl();
        }

        private async Task GoToLose()
        {
            _engine.displayLoadingUI();

            // Scene Setup
            _scene.detachControl();
            var scene = new Scene(
                _engine
            )
            {
                clearColor = new Color4(
                    0,
                    0,
                    0,
                    1
                )
            };
            var camera = new FreeCamera(
                "camera1",
                new Vector3(
                    0,
                    0,
                    0
                ),
                scene
            );
            scene.activeCamera = camera;
            camera.setTarget(Vector3.Zero());
            camera.attachControl(true);

            // GUI Setup
            var guiMenu = AdvancedDynamicTexture.CreateFullscreenUI(
                name: "UI",
                scene: scene
            );
            var mainButton = Button.CreateSimpleButton(
                "mainmenu",
                "MAIN MENU"
            );
            mainButton.width = "0.2";
            mainButton.height = "40px";
            mainButton.color = "white";
            guiMenu.addControl(mainButton);
            mainButton.onPointerUpObservable.add(async (_, __) =>
            {
                await GoToStart();
            });

            // Scene Finished Loading
            await scene.whenReadyAsync();
            _engine.hideLoadingUI();

            _scene.dispose();
            _scene = scene;
            _state = GameState.Lose;
        }

        private Task InitializeGame(Scene scene)
        {
            var light0 = new HemisphericLight(
                "HemiLight",
                new Vector3(0, 1, 0),
                scene
            );

            var light = new PointLight(
                "sparklight",
                new Vector3(0, 0, 0),
                scene
            )
            {
                diffuse = new Color3(
                    0.08627450980392157m,
                    0.10980392156862745m,
                    0.15294117647058825m
                ),
                intensity = 35,
                radius = 1
            };

            var shadowGenerator = new ShadowGenerator(
                1024,
                light
            )
            {
                darkness = 0.4m
            };

            _player = new Player(
                _assets,
                scene,
                shadowGenerator,
                _input
            );
            _player.ActivatePlayerCamera();

            return Task.CompletedTask;
        }
    }
}
