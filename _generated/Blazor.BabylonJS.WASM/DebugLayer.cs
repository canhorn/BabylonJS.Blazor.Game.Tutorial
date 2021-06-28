/// Generated - Do Not Edit
namespace BABYLON
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;
    using EventHorizon.Blazor.Interop.ResultCallbacks;
    using Microsoft.JSInterop;

    
    
    [JsonConverter(typeof(CachedEntityConverter<DebugLayer>))]
    public class DebugLayer : CachedEntityObject
    {
        #region Static Accessors

        #endregion

        #region Static Properties
        
        public static string InspectorURL
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    "BABYLON",
                    "DebugLayer.InspectorURL"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "DebugLayer.InspectorURL",
                    value
                );
            }
        }
        #endregion

        #region Static Methods

        #endregion

        #region Accessors
        
        public CachedEntity onPropertyChangedObservable
        {
            get
            {
            return EventHorizonBlazorInterop.Get<CachedEntity>(
                    this.___guid,
                    "onPropertyChangedObservable"
                );
            }
        }
        #endregion

        #region Properties

        #endregion
        
        #region Constructor
        public DebugLayer() : base() { }

        public DebugLayer(
            ICachedEntity entity
        ) : base(entity)
        {
            ___guid = entity.___guid;
        }

        public DebugLayer(
            Scene scene
        )
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "DebugLayer" },
                scene
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public void select(object entity, string lineContainerTitles = null)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "select" }, entity, lineContainerTitles
                }
            );
        }

        public bool isVisible()
        {
            return EventHorizonBlazorInterop.Func<bool>(
                new object[]
                {
                    new string[] { this.___guid, "isVisible" }
                }
            );
        }

        public void hide()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "hide" }
                }
            );
        }

        public void setAsActiveScene()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "setAsActiveScene" }
                }
            );
        }

        public ValueTask<DebugLayer> show(IInspectorOptions config = null)
        {
            return EventHorizonBlazorInterop.TaskClass<DebugLayer>(
                entity => new DebugLayer() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { this.___guid, "show" }, config
                }
            );
        }
        #endregion
    }
}