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
}
