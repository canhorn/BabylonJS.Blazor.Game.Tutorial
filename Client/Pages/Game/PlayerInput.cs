namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BABYLON;
    using EventHorizon.Blazor.Interop;

    public class PlayerInput
    {
        private Dictionary<string, bool> _inputMap;
        private readonly Hud _ui;

        public decimal Horizontal { get; set; }
        public decimal Vertical { get; set; }
        public decimal HorizontalAxis { get; set; }
        public decimal VerticalAxis { get; set; }
        public bool Dashing { get; set; }
        public bool JumpKeyDown { get; set; }
        public Dictionary<string, bool> InputMap => _inputMap;

        public PlayerInput(
            Scene scene,
            Hud ui
        )
        {
            _ui = ui;
            scene.actionManager = new ActionManager(scene);

            _inputMap = new Dictionary<string, bool>
            {
                ["ArrowUp"] = false,
                ["ArrowDown"] = false,
                ["ArrowLeft"] = false,
                ["ArrowRight"] = false,
                ["Shift"] = false,
                [" "] = false,
            };
            scene.actionManager.registerAction(
                new ExecuteCodeAction(
                    ActionManager.OnKeyDownTrigger,
                    new EventHorizon.Blazor.Interop.Callbacks.ActionCallback<ActionEvent>(
                        args =>
                        {
                            var key = GetKeyFromSourceEvent(args.sourceEvent);
                            var type = GetTypeFromSourceEvent(args.sourceEvent);
                            _inputMap[key] = type == "keydown";
                            return Task.CompletedTask;
                        }
                    )
                )
            );

            scene.actionManager.registerAction(
                new ExecuteCodeAction(
                    ActionManager.OnKeyUpTrigger,
                    new EventHorizon.Blazor.Interop.Callbacks.ActionCallback<ActionEvent>(
                        args =>
                        {
                            var key = GetKeyFromSourceEvent(args.sourceEvent);
                            var type = GetTypeFromSourceEvent(args.sourceEvent);
                            _inputMap[key] = type == "keydown";
                            return Task.CompletedTask;
                        }
                    )
                )
            );

            scene.onBeforeRenderObservable.add((scene, state) =>
            {
                UpdateFromKeyboard();
                return Task.CompletedTask;
            });
        }

        private void UpdateFromKeyboard()
        {
            if (_inputMap["ArrowUp"]
                && !_ui.GamePaused
            )
            {
                Vertical = Lerp(Vertical, 1, 0.2m);
                VerticalAxis = 1;
            }
            else if (_inputMap["ArrowDown"]
                && !_ui.GamePaused
            )
            {
                Vertical = Lerp(Vertical, -1, 0.2m);
                VerticalAxis = -1;
            }
            else
            {
                Vertical = 0;
                VerticalAxis = 0;
            }

            if (_inputMap["ArrowLeft"]
                && !_ui.GamePaused
            )
            {
                Horizontal = Lerp(Horizontal, -1, 0.2m);
                HorizontalAxis = -1;
            }
            else if (_inputMap["ArrowRight"]
                && !_ui.GamePaused
            )
            {
                Horizontal = Lerp(Horizontal, 1, 0.2m);
                HorizontalAxis = 1;
            }
            else
            {
                Horizontal = 0;
                HorizontalAxis = 0;
            }

            if (_inputMap["Shift"]
                && !_ui.GamePaused
            )
            {
                Dashing = true;
            }
            else
            {
                Dashing = false;
            }

            if (_inputMap[" "]
                && !_ui.GamePaused
            )
            {
                JumpKeyDown = true;
            }
            else
            {
                JumpKeyDown = false;
            }
        }

        private static decimal Lerp(decimal first, decimal second, decimal by)
        {
            return first * (1 - by) + second * by;
        }

        private static string GetKeyFromSourceEvent(
            CachedEntity entity
        )
        {
            return EventHorizonBlazorInterop.Get<string>(
                entity.___guid,
                "key"
            );
        }

        private static string GetTypeFromSourceEvent(
            CachedEntity entity
        )
        {
            return EventHorizonBlazorInterop.Get<string>(
                entity.___guid,
                "type"
            );
        }
    }
}
