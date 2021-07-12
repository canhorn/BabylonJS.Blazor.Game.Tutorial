namespace BabylonJS.Blazor.Game.Tutorial.Client.BabylonJSExtensions
{
    using System.Text.Json.Serialization;
    using BABYLON;
    using EventHorizon.Blazor.Interop;


    [JsonConverter(typeof(CachedEntityConverter<DebugLayerScene>))]
    public class DebugLayerScene : Scene
    {
        private DebugLayer __debugLayer;

        public DebugLayerScene(Engine engine, SceneOptions options = null)
            : base(engine, options)
        {
        }

        public DebugLayer debugLayer
        {
            get
            {
                if (__debugLayer == null)
                {
                    __debugLayer = EventHorizonBlazorInterop.GetClass<DebugLayer>(
                        this.___guid,
                        "debugLayer",
                        (entity) =>
                        {
                            return new DebugLayer() { ___guid = entity.___guid };
                        }
                    );
                }
                return __debugLayer;
            }
            set
            {
                __debugLayer = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "debugLayer",
                    value
                );
            }
        }
    }

    public static class TransformNodeExtensions
    {
        public static Vector3 forward(
            this TransformNode node
        ) => EventHorizonBlazorInterop.GetClass(
            node.___guid,
            "forward",
            (entity) => new Vector3() { ___guid = entity.___guid }
        );

        public static Vector3 right(
            this TransformNode node
        ) => EventHorizonBlazorInterop.GetClass(
            node.___guid,
            "right",
            (entity) => new Vector3() { ___guid = entity.___guid }
        );
    }

    public static class SceneExtensions
    {
        public static void stopAllAnimations(
            this Scene scene
        )
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { scene.___guid, "stopAllAnimations" },
                }
            );
        }
    }
}
