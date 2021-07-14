namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;
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
        private GameAssets _assets;
        private Player _player;
        private PlayerInput _input;
        private Hud _ui;

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
                async () =>
                {
                    switch (_state)
                    {
                        case GameState.Start:
                            _scene.render(true, false);
                            break;
                        case GameState.Game:
                            // once the timer 4 minutes, take us to the lose state
                            if (_ui.Time >= TimeSpan.FromMinutes(4)
                                && !_player.Win)
                            {
                                await GoToLose();
                                _ui.StopTimer();
                            }

                            if (_ui.Quit)
                            {
                                await GoToStart();
                                _ui.Quit = false;
                            }

                            _scene.render(true, false);
                            break;
                        case GameState.Lose:
                            _scene.render(true, false);
                            break;
                        case GameState.CutScene:
                            _scene.render(true, false);
                            break;
                        default:
                            break;
                    }
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

            // Load Character Assets
            _assets = await LoadCharacterAssets(scene);

            // Load Assets
            var environment = new GameEnvironment(scene);
            _environment = environment;
            await _environment.Load();

        }

        private async Task<GameAssets> LoadCharacterAssets(
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

            var result = await SceneLoader.ImportMeshAsync(
                null,
                "./models/",
                "player.glb",
                scene
            );

            var root = result.meshes[0];
            var body = root;
            body.parent = outer;
            body.isPickable = false;
            foreach (var mesh in body.getChildMeshes())
            {
                mesh.isPickable = false;
            }

            return new GameAssets
            {
                Mesh = outer,
                AnimationGroups = result.animationGroups,
            };
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
            var finishedLoading = false;
            _engine.displayLoadingUI();

            // Setup Scene
            _scene.detachControl();
            _cutScene = new Scene(
                _engine
            );


            var camera = new FreeCamera(
                "camera1",
                new Vector3(
                    0,
                    0,
                    0
                ),
                _cutScene
            );
            _cutScene.activeCamera = camera;
            camera.setTarget(Vector3.Zero());
            camera.attachControl(true);

            // --GUI--
            var cutScene = AdvancedDynamicTexture.CreateFullscreenUI(
                name: "cutscene",
                scene: _cutScene
            );
            var transition = 0;
            var canPlay = false;
            var finishedAnimation = false;
            var animationsLoaded = 0;
            var animationPlaying = 1;
            var dialogueTimer = new Timer(250);
            var animationTimer = new Timer(250);
            animationTimer.Stop();
            var animation2Timer = new Timer(750);
            animation2Timer.Stop();

            _cutScene.onDisposeObservable.add((_, __) =>
            {
                dialogueTimer.Dispose();
                animationTimer.Dispose();
                animation2Timer.Dispose();
                return Task.CompletedTask;
            });

            // Animations
            var beginning_anim = new Image(
                "sparkLife",
                "./sprites/beginning_anim.png"
            );
            beginning_anim.stretch = Image.STRETCH_UNIFORM;
            beginning_anim.cellId = 0;
            beginning_anim.cellHeight = 480;
            beginning_anim.cellWidth = 480;
            beginning_anim.sourceWidth = 480;
            beginning_anim.sourceHeight = 480;
            cutScene.addControl(beginning_anim);
            beginning_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });
            var working_anim = new Image("sparkLife", "./sprites/working_anim.png");
            working_anim.stretch = Image.STRETCH_UNIFORM;
            working_anim.cellId = 0;
            working_anim.cellHeight = 480;
            working_anim.cellWidth = 480;
            working_anim.sourceWidth = 480;
            working_anim.sourceHeight = 480;
            working_anim.isVisible = false;
            cutScene.addControl(working_anim);
            working_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });
            var dropoff_anim = new Image("sparkLife", "./sprites/dropoff_anim.png");
            dropoff_anim.stretch = Image.STRETCH_UNIFORM;
            dropoff_anim.cellId = 0;
            dropoff_anim.cellHeight = 480;
            dropoff_anim.cellWidth = 480;
            dropoff_anim.sourceWidth = 480;
            dropoff_anim.sourceHeight = 480;
            dropoff_anim.isVisible = false;
            cutScene.addControl(dropoff_anim);
            dropoff_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });
            var leaving_anim = new Image("sparkLife", "./sprites/leaving_anim.png");
            leaving_anim.stretch = Image.STRETCH_UNIFORM;
            leaving_anim.cellId = 0;
            leaving_anim.cellHeight = 480;
            leaving_anim.cellWidth = 480;
            leaving_anim.sourceWidth = 480;
            leaving_anim.sourceHeight = 480;
            leaving_anim.isVisible = false;
            cutScene.addControl(leaving_anim);
            leaving_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });
            var watermelon_anim = new Image("sparkLife", "./sprites/watermelon_anim.png");
            watermelon_anim.stretch = Image.STRETCH_UNIFORM;
            watermelon_anim.cellId = 0;
            watermelon_anim.cellHeight = 480;
            watermelon_anim.cellWidth = 480;
            watermelon_anim.sourceWidth = 480;
            watermelon_anim.sourceHeight = 480;
            watermelon_anim.isVisible = false;
            cutScene.addControl(watermelon_anim);
            watermelon_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });
            var reading_anim = new Image("sparkLife", "./sprites/reading_anim.png");
            reading_anim.stretch = Image.STRETCH_UNIFORM;
            reading_anim.cellId = 0;
            reading_anim.cellHeight = 480;
            reading_anim.cellWidth = 480;
            reading_anim.sourceWidth = 480;
            reading_anim.sourceHeight = 480;
            reading_anim.isVisible = false;
            cutScene.addControl(reading_anim);
            reading_anim.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });

            //Dialogue animations
            var dialogueBg = new Image("sparkLife", "./sprites/bg_anim_text_dialogue.png");
            dialogueBg.stretch = Image.STRETCH_UNIFORM;
            dialogueBg.cellId = 0;
            dialogueBg.cellHeight = 480;
            dialogueBg.cellWidth = 480;
            dialogueBg.sourceWidth = 480;
            dialogueBg.sourceHeight = 480;
            dialogueBg.horizontalAlignment = 0;
            dialogueBg.verticalAlignment = 0;
            dialogueBg.isVisible = false;
            cutScene.addControl(dialogueBg);
            dialogueBg.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });

            var dialogue = new Image("sparkLife", "./sprites/text_dialogue.png");
            dialogue.stretch = Image.STRETCH_UNIFORM;
            dialogue.cellId = 0;
            dialogue.cellHeight = 480;
            dialogue.cellWidth = 480;
            dialogue.sourceWidth = 480;
            dialogue.sourceHeight = 480;
            dialogue.horizontalAlignment = 0;
            dialogue.verticalAlignment = 0;
            dialogue.isVisible = false;
            cutScene.addControl(dialogue);
            dialogue.onImageLoadedObservable.add((_, __) =>
            {
                animationsLoaded++;
                return Task.CompletedTask;
            });


            // skip cutscene
            var skipBtn = Button.CreateSimpleButton("skip", "SKIP");
            skipBtn.fontFamily = "Viga";
            skipBtn.width = "45px";
            skipBtn.left = "-14px";
            skipBtn.height = "40px";
            skipBtn.color = "white";
            skipBtn.top = "14px";
            skipBtn.thickness = 0;
            skipBtn.verticalAlignment = Control.VERTICAL_ALIGNMENT_TOP;
            skipBtn.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_RIGHT;
            cutScene.addControl(skipBtn);

            skipBtn.onPointerDownObservable.add((_, __) =>
            {
                canPlay = true;

                return Task.CompletedTask;
            });

            // -- PLAYING ANIMATIONS --
            _cutScene.onBeforeRenderObservable.add(async (_, __) =>
            {
                if (animationsLoaded == 8)
                {
                    _engine.hideLoadingUI();
                    animationsLoaded = 0;

                    animationTimer.Start();
                    animation2Timer.Start();
                }

                if (finishedLoading
                    && canPlay
                )
                {
                    _cutScene.detachControl();
                    animationTimer.Dispose();
                    animation2Timer.Dispose();
                    dialogueTimer.Dispose();
                    _engine.displayLoadingUI();
                    canPlay = false;
                    await GoToGame();
                }
            });

            //--PROGRESS DIALOGUE--
            var next = Button.CreateImageOnlyButton(
                "next",
                "./sprites/arrowBtn.png"
            );
            next.rotation = (decimal)Math.PI / 2;
            next.thickness = 0;
            next.verticalAlignment = Control.VERTICAL_ALIGNMENT_BOTTOM;
            next.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_RIGHT;
            next.width = "64px";
            next.height = "64px";
            next.top = "-3%";
            next.left = "-12%";
            next.isVisible = false;
            cutScene.addControl(next);

            next.onPointerUpObservable.add((_, __) =>
            {
                if (transition == 8)
                {
                    // Once we reach the last dialogue frame, goToGame
                    _cutScene.detachControl();
                    // If the game hasn't loaded yet, we'll see a loading screen
                    _engine.displayLoadingUI();
                    transition = 0;
                    canPlay = true;
                }
                else if (transition < 8)
                {
                    // 8 frames of dialogue
                    transition++;
                    dialogue.cellId++;
                }

                return Task.CompletedTask;
            });

            // Looping animation for the dialogue background
            dialogueTimer.Elapsed += (_, __) =>
            {
                if (finishedAnimation
                    && dialogueBg.cellId < 3
                )
                {
                    dialogueBg.cellId++;
                }
                else
                {
                    dialogueBg.cellId = 0;
                }
            };
            // Keeps track of wich animation we are playing
            animationTimer.Elapsed += (_, __) =>
            {
                switch (animationPlaying)
                {
                    case 1:
                        if (beginning_anim.cellId == 9)
                        {
                            // Each animation could have a different number of frames
                            animationPlaying++;
                            // current animation hidden
                            beginning_anim.isVisible = false;
                            // show the next animation
                            working_anim.isVisible = true;
                        }
                        else
                        {
                            beginning_anim.cellId++;
                        }
                        break;
                    case 2:
                        if (working_anim.cellId == 11)
                        {
                            animationPlaying++;
                            working_anim.isVisible = false;
                            dropoff_anim.isVisible = true;
                        }
                        else
                        {
                            working_anim.cellId++;
                        }
                        break;
                    case 3:
                        if (dropoff_anim.cellId == 11)
                        {
                            animationPlaying++;
                            dropoff_anim.isVisible = false;
                            leaving_anim.isVisible = true;
                        }
                        else
                        {
                            dropoff_anim.cellId++;
                        }
                        break;
                    case 4:
                        if (leaving_anim.cellId == 9)
                        {
                            animationPlaying++;
                            leaving_anim.isVisible = false;
                            watermelon_anim.isVisible = true;
                        }
                        else
                        {
                            leaving_anim.cellId++;
                        }
                        break;
                    default:
                        break;
                }
            };

            animation2Timer.Elapsed += (_, __) =>
            {
                switch (animationPlaying)
                {
                    case 5:
                        if (watermelon_anim.cellId == 8)
                        {
                            animationPlaying++;
                            watermelon_anim.isVisible = false;
                            reading_anim.isVisible = true;
                        }
                        else
                        {
                            watermelon_anim.cellId++;
                        }
                        break;
                    case 6:
                        if (reading_anim.cellId == 11)
                        {
                            reading_anim.isVisible = false;
                            finishedAnimation = true;
                            dialogueBg.isVisible = true;
                            dialogue.isVisible = true;
                            next.isVisible = true;
                        }
                        else
                        {
                            reading_anim.cellId++;
                        }
                        break;
                }
            };

            // Scene Finished Loading
            await _cutScene.whenReadyAsync();
            _scene.dispose();
            _state = GameState.CutScene;
            _scene = _cutScene;

            _engine.hideLoadingUI();

            // Start Loading and Setup the Game
            await SetupGame();
            finishedLoading = true;
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
            var ui = new Hud(scene);
            _ui = ui;
            //dont detect any inputs from this ui while the game is loading
            scene.detachControl();

            // --INPUT--
            _input = new PlayerInput(
                scene,
                _ui
            );

            // Primitive Character and Settings
            await InitializeGame(scene);

            // -- WHEN SCENE FINISHED LOADING --
            await scene.whenReadyAsync();
            scene.getMeshByName(
                "outer"
            ).position = scene.getTransformNodeByID(
                "startPosition"
            ).getAbsolutePosition();
            // Setup the game timer and sparkler timer -- linked to the ui
            _ui.StartTimer();
            _ui.StartSparklerTimer(_player.Sparkler);

            //get rid of start scene, switch to gamescene and change states
            _scene.dispose();
            _state = GameState.Game;
            _scene = scene;
            _engine.hideLoadingUI();

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
                intensity = 0.2m,
                radius = 1
            };

            var shadowGenerator = new ShadowGenerator(
                1024,
                light,
                true
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

            // Setup lantern collision checks
            _environment.CheckLanterns(
                _player
            );

            //--Transition post process--
            scene.registerBeforeRender(new ActionCallback(() =>
            {
                if (_ui.Transition)
                {
                    _ui.FadeLevel -= 0.05m;

                    // Once the fade transition has complete, switch scenes
                    if (_ui.FadeLevel <= 0)
                    {
                        _ui.Quit = true;
                        _ui.Transition = false;
                    }
                }

                return Task.CompletedTask;
            }));

            // -- GAME LOOP --
            scene.onBeforeRenderObservable.add((_, __) =>
            {
                // Reset the Sparkler Timer
                if (_player.SparkReset)
                {
                    _ui.StartSparklerTimer(
                        _player.Sparkler
                    );
                    _player.SparkReset = false;

                    _ui.UpdateLanternCount(
                        _player.LanternsLit
                    );
                }

                // Stop the sparkler timer after 20 seconds
                else if (_ui.StopSpark
                    && _player.SparkLit
                )
                {
                    _ui.StopSparklerTimer(
                        _player.Sparkler
                    );
                    _player.SparkLit = false;
                }

                // If you have reached the destination and lit all the lanterns
                if (_player.Win
                    && _player.LanternsLit == 22
                )
                {
                    // Stop the timer so that fireworks can play and player cant move around
                    _ui.GamePaused = true;
                    // Dont allow pause menu interation
                    _ui.PauseButton.isHitTestVisible = false;

                    // 10 seconds
                    var i = 10;
                    var timer = new Timer(1000);
                    timer.Elapsed += (_, __) =>
                    {
                        i--;
                        if (i == 0)
                        {
                            ShowWin();
                        }
                    };
                    timer.Start();

                    _environment.StartFireworks = true;
                    _player.Win = false;
                }

                // When the game isn't paused, update the timer
                if (!_ui.GamePaused)
                {
                    _ui.UpdateHub();
                }

                return Task.CompletedTask;
            });

            _environment.Initialize();

            return Task.CompletedTask;
        }

        private void ShowWin()
        {
            var winUI = AdvancedDynamicTexture.CreateFullscreenUI("UI");
            winUI.idealHeight = 720;

            var rect = new Rectangle("show-win");
            rect.thickness = 0;
            rect.background = "black";
            rect.alpha = 0.4m;
            rect.width = "0.4";
            winUI.addControl(rect);

            var stackPanel = new StackPanel("credits");
            stackPanel.width = "0.4";
            stackPanel.fontFamily = "Viga";
            stackPanel.fontSize = "16px";
            stackPanel.color = "white";
            winUI.addControl(stackPanel);

            var wincreds = new TextBlock("special");
            wincreds.resizeToFit = true;
            wincreds.color = "white";
            wincreds.text = "Special thanks to the Babylon Team!";
            wincreds.textWrapping = 1;
            wincreds.height = "24px";
            wincreds.width = "100%";
            wincreds.fontFamily = "Viga";
            stackPanel.addControl(wincreds);

            // Credits for music & SFX
            var music = new TextBlock("music", "Music");
            music.fontSize = "22";
            music.resizeToFit = true;
            music.textWrapping = 1;

            var source = new TextBlock(
                "sources",
                "Sources: freesound.org, opengameart.org, and itch.io"
            );
            source.textWrapping = 1;
            source.resizeToFit = true;

            var jumpCred = new TextBlock("jumpCred", "jump2 by LloydEvans09 - freesound.org");
            jumpCred.textWrapping = 1;
            jumpCred.resizeToFit = true;

            var walkCred = new TextBlock("walkCred", "Concrete 2 by MayaSama @mayasama.itch.io / ig: @mayaragandra");
            walkCred.textWrapping = 1;
            walkCred.resizeToFit = true;

            var gameCred = new TextBlock("gameSong", "Christmas synths by 3xBlast - opengameart.org");
            gameCred.textWrapping = 1;
            gameCred.resizeToFit = true;

            var pauseCred = new TextBlock("pauseSong", "Music by Matthew Pablo / www.matthewpablo.com - opengameart.org");
            pauseCred.textWrapping = 1;
            pauseCred.resizeToFit = true;

            var endCred = new TextBlock("startendSong", "copycat by syncopika - opengameart.org");
            endCred.textWrapping = 1;
            endCred.resizeToFit = true;

            var loseCred = new TextBlock("loseSong", "Eye of the Storm by Joth - opengameart.org");
            loseCred.textWrapping = 1;
            loseCred.resizeToFit = true;

            var fireworksSfx = new TextBlock("fireworks", "rubberduck - opengameart.org");
            fireworksSfx.textWrapping = 1;
            fireworksSfx.resizeToFit = true;

            var dashCred = new TextBlock("dashCred", "Woosh Noise 1 by potentjello - freesound.org");
            dashCred.textWrapping = 1;
            dashCred.resizeToFit = true;

            //  quit, sparkwarning, reset
            var sfxCred = new TextBlock("sfxCred", "200 Free SFX - https://kronbits.itch.io/freesfx");
            sfxCred.textWrapping = 1;
            sfxCred.resizeToFit = true;

            // lighting lantern, sparkreset
            var sfxCred2 = new TextBlock("sfxCred2", "sound pack by wobbleboxx.com - opengameart.org");
            sfxCred2.textWrapping = 1;
            sfxCred2.resizeToFit = true;

            var selectionSfxCred = new TextBlock("select", "8bit menu select by Fupi - opengameart.org");
            selectionSfxCred.textWrapping = 1;
            selectionSfxCred.resizeToFit = true;

            stackPanel.addControl(music);
            stackPanel.addControl(source);
            stackPanel.addControl(jumpCred);
            stackPanel.addControl(walkCred);
            stackPanel.addControl(gameCred);
            stackPanel.addControl(pauseCred);
            stackPanel.addControl(endCred);
            stackPanel.addControl(loseCred);
            stackPanel.addControl(fireworksSfx);
            stackPanel.addControl(dashCred);
            stackPanel.addControl(sfxCred);
            stackPanel.addControl(sfxCred2);
            stackPanel.addControl(selectionSfxCred);

            var mainMenu = Button.CreateSimpleButton("mainmenu", "RETURN");
            mainMenu.verticalAlignment = Control.VERTICAL_ALIGNMENT_BOTTOM;
            mainMenu.fontFamily = "Viga";
            mainMenu.width = "0.2";
            mainMenu.height = "40px";
            mainMenu.color = "white";
            winUI.addControl(mainMenu);

            mainMenu.onPointerDownObservable.add((_, __) =>
            {
                _ui.Transition = true;

                return Task.CompletedTask;
            });
        }
    }
}
