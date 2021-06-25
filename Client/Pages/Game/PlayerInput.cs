namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BABYLON;
    using EventHorizon.Blazor.Interop;

    public class PlayerInput
    {
        private Dictionary<string, bool> _inputMap;

        public decimal Horizontal { get; set; }
        public decimal Vertical { get; set; }
        public decimal HorizontalAxis { get; set; }
        public decimal VerticalAxis { get; set; }

        public PlayerInput(
            Scene scene
        )
        {
            scene.actionManager = new ActionManager(scene);

            _inputMap = new Dictionary<string, bool>
            {
                ["ArrowUp"] = false,
                ["ArrowDown"] = false,
                ["ArrowLeft"] = false,
                ["ArrowRight"] = false,
            };
            scene.actionManager.registerAction(
                new ExecuteCodeAction(
                    ActionManager.OnKeyDownTrigger,
                    new EventHorizon.Blazor.Interop.Callbacks.ActionCallback<ActionEvent>(
                        args =>
                        {
                            System.Console.WriteLine("hi");
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
            if (_inputMap["ArrowUp"])
            {
                Vertical = Lerp(Vertical, 1, 0.2m);
                VerticalAxis = 1;
            }
            else if (_inputMap["ArrowDown"])
            {
                Vertical = Lerp(Vertical, -1, 0.2m);
                VerticalAxis = -1;
            }
            else
            {
                Vertical = 0;
                VerticalAxis = 0;
            }

            if (_inputMap["ArrowLeft"])
            {
                Horizontal = Lerp(Horizontal, -1, 0.2m);
                HorizontalAxis = -1;
            }
            else if (_inputMap["ArrowRight"])
            {
                Horizontal = Lerp(Horizontal, 1, 0.2m);
                HorizontalAxis = 1;
            }
            else
            {
                Horizontal = 0;
                HorizontalAxis = 0;
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
