namespace BabylonJS.Blazor.Game.Tutorial.Client.HTML
{
    using EventHorizon.Blazor.Interop;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(CachedEntityConverter<Canvas>))]
    public class Canvas : HTMLCanvasElementCachedEntity
    {
        public static Canvas GetElementById(
            string elementId
        ) => EventHorizonBlazorInterop.FuncClass(
            entity => new Canvas(entity),
            new string[] { "document", "getElementById" },
            elementId
        );

        private Canvas(
            ICachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }

        /// <summary>
        /// We use this to reset the focus state of the Canvas.
        /// This helps with an issue with the ActionManager not capturing actions.
        /// </summary>
        public void ResetControl()
        {
            blur();
            focus();
        }

        public void blur()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "blur" }
                }
            );
        }

        public void focus()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "focus" }
                }
            );
        }
    }
}
