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

    
    
    [JsonConverter(typeof(CachedEntityConverter<GlowLayer>))]
    public class GlowLayer : EffectLayer
    {
        #region Static Accessors

        #endregion

        #region Static Properties
        
        public static string EffectName
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    "BABYLON",
                    "GlowLayer.EffectName"
                );
            }
        }

        
        public static decimal DefaultBlurKernelSize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    "BABYLON",
                    "GlowLayer.DefaultBlurKernelSize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "GlowLayer.DefaultBlurKernelSize",
                    value
                );
            }
        }

        
        public static decimal DefaultTextureRatio
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    "BABYLON",
                    "GlowLayer.DefaultTextureRatio"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "GlowLayer.DefaultTextureRatio",
                    value
                );
            }
        }
        #endregion

        #region Static Methods
        public static GlowLayer Parse(object parsedGlowLayer, Scene scene, string rootUrl)
        {
            return EventHorizonBlazorInterop.FuncClass<GlowLayer>(
                entity => new GlowLayer() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "GlowLayer", "Parse" }, parsedGlowLayer, scene, rootUrl
                }
            );
        }
        #endregion

        #region Accessors
        
        public decimal blurKernelSize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "blurKernelSize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "blurKernelSize",
                    value
                );
            }
        }

        
        public decimal intensity
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "intensity"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "intensity",
                    value
                );
            }
        }
        #endregion

        #region Properties

        #endregion
        
        #region Constructor
        public GlowLayer() : base() { }

        public GlowLayer(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public GlowLayer(
            string name, Scene scene, IGlowLayerOptions options = null
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "GlowLayer" },
                name, scene, options
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        #region customEmissiveColorSelector TODO: Get Comments as metadata identification
        private bool _isCustomEmissiveColorSelectorEnabled = false;
        private readonly IDictionary<string, Func<Task>> _customEmissiveColorSelectorActionMap = new Dictionary<string, Func<Task>>();

        public string customEmissiveColorSelector(
            Func<Task> callback
        )
        {
            SetupCustomEmissiveColorSelectorLoop();

            var handle = Guid.NewGuid().ToString();
            _customEmissiveColorSelectorActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool customEmissiveColorSelector_Remove(
            string handle
        )
        {
            return _customEmissiveColorSelectorActionMap.Remove(
                handle
            );
        }

        private void SetupCustomEmissiveColorSelectorLoop()
        {
            if (_isCustomEmissiveColorSelectorEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "customEmissiveColorSelector",
                "CallCustomEmissiveColorSelectorActions",
                _invokableReference
            );
            _isCustomEmissiveColorSelectorEnabled = true;
        }

        [JSInvokable]
        public async Task CallCustomEmissiveColorSelectorActions()
        {
            foreach (var action in _customEmissiveColorSelectorActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        #region customEmissiveTextureSelector TODO: Get Comments as metadata identification
        private bool _isCustomEmissiveTextureSelectorEnabled = false;
        private readonly IDictionary<string, Func<Task>> _customEmissiveTextureSelectorActionMap = new Dictionary<string, Func<Task>>();

        public string customEmissiveTextureSelector(
            Func<Task> callback
        )
        {
            SetupCustomEmissiveTextureSelectorLoop();

            var handle = Guid.NewGuid().ToString();
            _customEmissiveTextureSelectorActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool customEmissiveTextureSelector_Remove(
            string handle
        )
        {
            return _customEmissiveTextureSelectorActionMap.Remove(
                handle
            );
        }

        private void SetupCustomEmissiveTextureSelectorLoop()
        {
            if (_isCustomEmissiveTextureSelectorEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "customEmissiveTextureSelector",
                "CallCustomEmissiveTextureSelectorActions",
                _invokableReference
            );
            _isCustomEmissiveTextureSelectorEnabled = true;
        }

        [JSInvokable]
        public async Task CallCustomEmissiveTextureSelectorActions()
        {
            foreach (var action in _customEmissiveTextureSelectorActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        public string getEffectName()
        {
            return EventHorizonBlazorInterop.Func<string>(
                new object[]
                {
                    new string[] { this.___guid, "getEffectName" }
                }
            );
        }

        public bool isReady(SubMesh subMesh, bool useInstances)
        {
            return EventHorizonBlazorInterop.Func<bool>(
                new object[]
                {
                    new string[] { this.___guid, "isReady" }, subMesh, useInstances
                }
            );
        }

        public bool needStencil()
        {
            return EventHorizonBlazorInterop.Func<bool>(
                new object[]
                {
                    new string[] { this.___guid, "needStencil" }
                }
            );
        }

        public void addExcludedMesh(Mesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "addExcludedMesh" }, mesh
                }
            );
        }

        public void removeExcludedMesh(Mesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "removeExcludedMesh" }, mesh
                }
            );
        }

        public void addIncludedOnlyMesh(Mesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "addIncludedOnlyMesh" }, mesh
                }
            );
        }

        public void removeIncludedOnlyMesh(Mesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "removeIncludedOnlyMesh" }, mesh
                }
            );
        }

        public bool hasMesh(AbstractMesh mesh)
        {
            return EventHorizonBlazorInterop.Func<bool>(
                new object[]
                {
                    new string[] { this.___guid, "hasMesh" }, mesh
                }
            );
        }

        public void referenceMeshToUseItsOwnMaterial(AbstractMesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "referenceMeshToUseItsOwnMaterial" }, mesh
                }
            );
        }

        public void unReferenceMeshFromUsingItsOwnMaterial(AbstractMesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "unReferenceMeshFromUsingItsOwnMaterial" }, mesh
                }
            );
        }

        public string getClassName()
        {
            return EventHorizonBlazorInterop.Func<string>(
                new object[]
                {
                    new string[] { this.___guid, "getClassName" }
                }
            );
        }

        public CachedEntity serialize()
        {
            return EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "serialize" }
                }
            );
        }
        #endregion
    }
}