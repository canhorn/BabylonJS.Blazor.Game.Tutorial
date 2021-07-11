namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;
    using BABYLON;
    using BABYLON.GUI;

    public class Hud
    {
        private readonly Scene _scene;

        // Game Timer
        private TimeSpan _prevTime;
        private TextBlock _clockTime;
        private DateTime? _startTime;
        private bool _stopTimer;
        private string _secondString = "00";
        private string _minuteString = "11";
        private TextBlock _lanternCount;

        // Animated UI Sprites
        private readonly Image _sparklerLife;
        private readonly Image _spark;

        // Timer Handlers
        private readonly Timer _timer;
        private readonly Timer _sparkTimer;

        // UI Elements
        private readonly AdvancedDynamicTexture _playerUI;
        private Rectangle _pauseMenu;
        private Rectangle _controls;

        // UI
        public AdvancedDynamicTexture Root => _playerUI;

        // Game Timer
        public TimeSpan Time { get; set; }

        // Timer Handlers
        public bool StopSpark { get; set; }

        // Pause Toggle
        public bool GamePaused { get; set; }

        // Quit Game
        public bool Quit { get; set; }
        public bool Transition { get; set; }

        // UI Elements
        public Button PauseButton { get; private set; }
        public decimal FadeLevel { get; private set; }

        public Hud(
            Scene scene
        )
        {
            _scene = scene;

            var playerUI = AdvancedDynamicTexture.CreateFullscreenUI(
                "UI",
                foreground: true,
                _scene
            );
            _playerUI = playerUI;
            _playerUI.idealHeight = 720;

            var lanternCount = new TextBlock("Lantern Count", "Lanterns: 1/22")
            {
                textVerticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER,
                horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_RIGHT,
                verticalAlignment = Control.VERTICAL_ALIGNMENT_TOP,
                fontSize = "22px",
                color = "white",
                top = "32px",
                left = "-64px",
                width = "25%",
                fontFamily = "Viga",
                resizeToFit = true,
            };
            playerUI.addControl(lanternCount);
            _lanternCount = lanternCount;

            var stackPanel = new StackPanel("stack-panel")
            {
                height = "100%",
                width = "100%",
                top = "14px",
                verticalAlignment = 0
            };
            playerUI.addControl(stackPanel);

            //Game timer text
            var clockTime = new TextBlock("clock")
            {
                textHorizontalAlignment = TextBlock.HORIZONTAL_ALIGNMENT_CENTER,
                fontSize = "48px",
                color = "white",
                text = "11:00",
                resizeToFit = true,
                height = "96px",
                width = "220px",
                fontFamily = "Viga"
            };
            stackPanel.addControl(clockTime);
            _clockTime = clockTime;

            //sparkler bar animation
            var sparklerLife = new Image("sparkLife", "./sprites/sparkLife.png")
            {
                width = "54px",
                height = "162px",
                cellId = 0,
                cellHeight = 108,
                cellWidth = 36,
                sourceWidth = 36,
                sourceHeight = 108,
                horizontalAlignment = 0,
                verticalAlignment = 0,
                left = "14px",
                top = "14px"
            };
            playerUI.addControl(sparklerLife);
            _sparklerLife = sparklerLife;

            var spark = new Image("spark", "./sprites/spark.png")
            {
                width = "40px",
                height = "40px",
                cellId = 0,
                cellHeight = 20,
                cellWidth = 20,
                sourceWidth = 20,
                sourceHeight = 20,
                horizontalAlignment = 0,
                verticalAlignment = 0,
                left = "21px",
                top = "20px"
            };
            playerUI.addControl(spark);
            _spark = spark;

            var pauseButton = Button.CreateImageOnlyButton("pauseBtn", "./sprites/pauseBtn.png");
            pauseButton.width = "48px";
            pauseButton.height = "86px";
            pauseButton.thickness = 0;
            pauseButton.verticalAlignment = 0;
            pauseButton.horizontalAlignment = 1;
            pauseButton.top = "-16px";
            playerUI.addControl(pauseButton);
            pauseButton.zIndex = 10;
            PauseButton = pauseButton;
            //when the button is down, make pause menu visable and add control to it
            pauseButton.onPointerDownObservable.add((_, __) =>
            {
                _pauseMenu.isVisible = true;
                playerUI.addControl(_pauseMenu);
                PauseButton.isHitTestVisible = false;

                //when game is paused, make sure that the next start time is the time it was when paused
                GamePaused = true;
                _prevTime = Time;

                return Task.CompletedTask;
            });

            CreatePauseMenu();
            CreateControlsMenu();

            _timer = new Timer(2000);
            _timer.Stop();
            _timer.Elapsed += HandleTimerElapsed;

            _sparkTimer = new Timer(185);
            _sparkTimer.Stop();
            _sparkTimer.Elapsed += HandleSparkTimerElapsed;

            scene.onDisposeObservable.add((_, __) =>
            {
                _timer.Dispose();
                _sparkTimer.Dispose();

                return Task.CompletedTask;
            });
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!GamePaused)
            {
                if (_sparklerLife.cellId < 10)
                {
                    _sparklerLife.cellId++;
                }

                if (_sparklerLife.cellId == 10)
                {
                    StopSpark = true;
                    _timer.Stop();
                }
            }
        }

        private void HandleSparkTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!GamePaused)
            {
                if (_sparklerLife.cellId < 10
                    && _spark.cellId < 5
                )
                {
                    _spark.cellId++;
                }
                else if (_sparklerLife.cellId < 10
                    && _spark.cellId >= 5
                )
                {
                    _spark.cellId = 0;
                }
                else
                {
                    _spark.cellId = 0;
                    _sparkTimer.Stop();
                }
            }
        }


        public void UpdateHub()
        {
            if (!_stopTimer
                && _startTime.HasValue
            )
            {
                var currentTime = DateTime.Now - _startTime.Value;

                Time = currentTime;
                _clockTime.text = FormatTime(currentTime);
            }
        }

        public void UpdateLanternCount(
            int numberOfLanterns
        )
        {
            _lanternCount.text = "Lanterns: " + numberOfLanterns + " / 22";
        }

        // Game Timer
        public void StartTimer()
        {
            _startTime = DateTime.Now;
            _stopTimer = false;
        }
        public void StopTimer()
        {
            _stopTimer = true;
        }

        /// <summary>
        /// Format the time so it is releative to 11:00 -- Game Time
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private string FormatTime(
            TimeSpan currentTime
        )
        {
            var minutesPassed = Math.Floor(
                (decimal)currentTime.Minutes / 4
            ) + 11;
            var secondsPassed = ((currentTime.Minutes * 60) + currentTime.Seconds) % 240;

            _minuteString = $"{minutesPassed}";
            _secondString = (secondsPassed / 4 < 10 ? "0" : "") + secondsPassed / 4;

            var day = _minuteString == "11" ? "PM" : "AM";
            return $"{_minuteString}:{_secondString}{day}";
        }

        #region Sparkler Timers
        /// <summary>
        /// Start/Restart Sparkler, handles setting the texture and animation frame
        /// </summary>
        public void StartSparklerTimer()
        {
            // Reset the sparkler timers and Animation Frames
            StopSpark = false;
            _sparklerLife.cellId = 0;
            _spark.cellId = 0;

            _scene.getLightByName("sparklight").intensity = 60;

            _timer.Start();

            _sparkTimer.Start();
        }

        /// <summary>
        /// Stop the Sparkler, resets the texture.
        /// </summary>
        public void StopSparklerTimer()
        {
            StopSpark = true;
            _scene.getLightByName("sparklight").intensity = 0;
        }
        #endregion

        /// <summary>
        /// Pause Menu Popup
        /// </summary>
        private void CreatePauseMenu()
        {
            GamePaused = false;

            var pauseMenu = new Rectangle("pause-menu")
            {
                horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_CENTER,
                verticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER,
                height = "0.8",
                width = "0.5",
                thickness = 0,
                isVisible = false
            };

            //background image
            var image = new Image("pause", "sprites/pause.jpeg");
            pauseMenu.addControl(image);

            //stack panel for the buttons
            var stackPanel = new StackPanel("pause-menu__stack-panel")
            {
                width = ".83"
            };
            pauseMenu.addControl(stackPanel);

            var resumeButton = Button.CreateSimpleButton("resume", "RESUME");
            resumeButton.width = "0.18";
            resumeButton.height = "44px";
            resumeButton.color = "white";
            resumeButton.fontFamily = "Viga";
            resumeButton.paddingBottom = "14px";
            resumeButton.cornerRadius = 14;
            resumeButton.fontSize = "12px";
            resumeButton.textBlock.resizeToFit = true;
            resumeButton.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_LEFT;
            resumeButton.verticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER;
            stackPanel.addControl(resumeButton);
            _pauseMenu = pauseMenu;

            //when the button is down, make menu invisable and remove control of the menu
            resumeButton.onPointerDownObservable.add((_, __) =>
            {
                _pauseMenu.isVisible = false;
                _playerUI.removeControl(pauseMenu);
                PauseButton.isHitTestVisible = true;

                //game unpaused, our time is now reset
                GamePaused = false;
                _startTime = DateTime.Now;

                return Task.CompletedTask;
            });

            var controlsBtn = Button.CreateSimpleButton("controls", "CONTROLS");
            controlsBtn.width = "0.18";
            controlsBtn.height = "44px";
            controlsBtn.color = "white";
            controlsBtn.fontFamily = "Viga";
            controlsBtn.paddingBottom = "14px";
            controlsBtn.cornerRadius = 14;
            controlsBtn.fontSize = "12px";
            resumeButton.textBlock.resizeToFit = true;
            controlsBtn.verticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER;
            controlsBtn.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_LEFT;

            stackPanel.addControl(controlsBtn);

            //when the button is down, make menu invisable and remove control of the menu
            controlsBtn.onPointerDownObservable.add((_, __) =>
            {
                //open controls screen
                _controls.isVisible = true;
                _pauseMenu.isVisible = false;

                return Task.CompletedTask;
            });

            var quitBtn = Button.CreateSimpleButton("quit", "QUIT");
            quitBtn.width = "0.18";
            quitBtn.height = "44px";
            quitBtn.color = "white";
            quitBtn.fontFamily = "Viga";
            quitBtn.paddingBottom = "12px";
            quitBtn.cornerRadius = 14;
            quitBtn.fontSize = "12px";
            resumeButton.textBlock.resizeToFit = true;
            quitBtn.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_LEFT;
            quitBtn.verticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER;
            stackPanel.addControl(quitBtn);

            //set up transition effect
            Effect.RegisterShader("fade",
                "precision highp float;" +
                "varying vec2 vUV;" +
                "uniform sampler2D textureSampler; " +
                "uniform float fadeLevel; " +
                "void main(void){" +
                "vec4 baseColor = texture2D(textureSampler, vUV) * fadeLevel;" +
                "baseColor.a = 1.0;" +
                "gl_FragColor = baseColor;" +
                "}");
            FadeLevel = 1.0m;

            quitBtn.onPointerDownObservable.add((_, __) =>
            {
                var postProcess = new PostProcess(
                    "Fade",
                    "fade",
                    options: 1.0m,
                    parameters: new[] { "fadeLevel" },
                    samplers: null,
                    camera: _scene.getCameraByName("cam")
                );
                postProcess.onApplyObservable.add((effect, _) =>
                {
                    effect.setFloat("fadeLevel", FadeLevel);

                    return Task.CompletedTask;
                });
                Transition = true;

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Controls Menu Popup
        /// </summary>
        private void CreateControlsMenu()
        {
            var controls = new Rectangle("controls__rectangle")
            {
                horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_CENTER,
                verticalAlignment = Control.VERTICAL_ALIGNMENT_CENTER,
                height = "0.8",
                width = "0.5",
                thickness = 0,
                color = "white",
                isVisible = false
            };
            _playerUI.addControl(controls);
            _controls = controls;

            //background image
            var image = new Image("controls", "sprites/controls.jpeg");
            controls.addControl(image);

            var title = new TextBlock("title", "CONTROLS")
            {
                resizeToFit = true,
                verticalAlignment = Control.VERTICAL_ALIGNMENT_TOP,
                fontFamily = "Viga",
                fontSize = "32px",
                top = "14px"
            };
            controls.addControl(title);

            var backBtn = Button.CreateImageOnlyButton("back", "./sprites/lanternbutton.jpeg");
            backBtn.width = "40px";
            backBtn.height = "40px";
            backBtn.top = "14px";
            backBtn.thickness = 0;
            backBtn.horizontalAlignment = Control.HORIZONTAL_ALIGNMENT_RIGHT;
            backBtn.verticalAlignment = Control.VERTICAL_ALIGNMENT_TOP;
            controls.addControl(backBtn);

            //when the button is down, make menu invisable and remove control of the menu
            backBtn.onPointerDownObservable.add((_, __) =>
            {
                _pauseMenu.isVisible = true;
                _controls.isVisible = false;

                return Task.CompletedTask;
            });
        }
    }
}
