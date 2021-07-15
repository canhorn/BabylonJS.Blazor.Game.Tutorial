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

    
    
    [JsonConverter(typeof(CachedEntityConverter<EffectLayer>))]
    public class EffectLayer : CachedEntityObject
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static EffectLayer Parse(object parsedEffectLayer, Scene scene, string rootUrl)
        {
            return EventHorizonBlazorInterop.FuncClass<EffectLayer>(
                entity => new EffectLayer() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "EffectLayer", "Parse" }, parsedEffectLayer, scene, rootUrl
                }
            );
        }
        #endregion

        #region Accessors
        private Camera __camera;
        public Camera camera
        {
            get
            {
            if(__camera == null)
            {
                __camera = EventHorizonBlazorInterop.GetClass<Camera>(
                    this.___guid,
                    "camera",
                    (entity) =>
                    {
                        return new Camera() { ___guid = entity.___guid };
                    }
                );
            }
            return __camera;
            }
        }

        
        public decimal renderingGroupId
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "renderingGroupId"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "renderingGroupId",
                    value
                );
            }
        }
        #endregion

        #region Properties
        
        public string name
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    this.___guid,
                    "name"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "name",
                    value
                );
            }
        }

        private Color4 __neutralColor;
        public Color4 neutralColor
        {
            get
            {
            if(__neutralColor == null)
            {
                __neutralColor = EventHorizonBlazorInterop.GetClass<Color4>(
                    this.___guid,
                    "neutralColor",
                    (entity) =>
                    {
                        return new Color4() { ___guid = entity.___guid };
                    }
                );
            }
            return __neutralColor;
            }
            set
            {
__neutralColor = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "neutralColor",
                    value
                );
            }
        }

        
        public bool isEnabled
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "isEnabled"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "isEnabled",
                    value
                );
            }
        }

        
        public bool disableBoundingBoxesFromEffectLayer
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "disableBoundingBoxesFromEffectLayer"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "disableBoundingBoxesFromEffectLayer",
                    value
                );
            }
        }

        private Observable<EffectLayer> __onDisposeObservable;
        public Observable<EffectLayer> onDisposeObservable
        {
            get
            {
            if(__onDisposeObservable == null)
            {
                __onDisposeObservable = EventHorizonBlazorInterop.GetClass<Observable<EffectLayer>>(
                    this.___guid,
                    "onDisposeObservable",
                    (entity) =>
                    {
                        return new Observable<EffectLayer>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onDisposeObservable;
            }
            set
            {
__onDisposeObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onDisposeObservable",
                    value
                );
            }
        }

        private Observable<EffectLayer> __onBeforeRenderMainTextureObservable;
        public Observable<EffectLayer> onBeforeRenderMainTextureObservable
        {
            get
            {
            if(__onBeforeRenderMainTextureObservable == null)
            {
                __onBeforeRenderMainTextureObservable = EventHorizonBlazorInterop.GetClass<Observable<EffectLayer>>(
                    this.___guid,
                    "onBeforeRenderMainTextureObservable",
                    (entity) =>
                    {
                        return new Observable<EffectLayer>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onBeforeRenderMainTextureObservable;
            }
            set
            {
__onBeforeRenderMainTextureObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onBeforeRenderMainTextureObservable",
                    value
                );
            }
        }

        private Observable<EffectLayer> __onBeforeComposeObservable;
        public Observable<EffectLayer> onBeforeComposeObservable
        {
            get
            {
            if(__onBeforeComposeObservable == null)
            {
                __onBeforeComposeObservable = EventHorizonBlazorInterop.GetClass<Observable<EffectLayer>>(
                    this.___guid,
                    "onBeforeComposeObservable",
                    (entity) =>
                    {
                        return new Observable<EffectLayer>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onBeforeComposeObservable;
            }
            set
            {
__onBeforeComposeObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onBeforeComposeObservable",
                    value
                );
            }
        }

        private Observable<AbstractMesh> __onBeforeRenderMeshToEffect;
        public Observable<AbstractMesh> onBeforeRenderMeshToEffect
        {
            get
            {
            if(__onBeforeRenderMeshToEffect == null)
            {
                __onBeforeRenderMeshToEffect = EventHorizonBlazorInterop.GetClass<Observable<AbstractMesh>>(
                    this.___guid,
                    "onBeforeRenderMeshToEffect",
                    (entity) =>
                    {
                        return new Observable<AbstractMesh>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onBeforeRenderMeshToEffect;
            }
            set
            {
__onBeforeRenderMeshToEffect = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onBeforeRenderMeshToEffect",
                    value
                );
            }
        }

        private Observable<AbstractMesh> __onAfterRenderMeshToEffect;
        public Observable<AbstractMesh> onAfterRenderMeshToEffect
        {
            get
            {
            if(__onAfterRenderMeshToEffect == null)
            {
                __onAfterRenderMeshToEffect = EventHorizonBlazorInterop.GetClass<Observable<AbstractMesh>>(
                    this.___guid,
                    "onAfterRenderMeshToEffect",
                    (entity) =>
                    {
                        return new Observable<AbstractMesh>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onAfterRenderMeshToEffect;
            }
            set
            {
__onAfterRenderMeshToEffect = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onAfterRenderMeshToEffect",
                    value
                );
            }
        }

        private Observable<EffectLayer> __onAfterComposeObservable;
        public Observable<EffectLayer> onAfterComposeObservable
        {
            get
            {
            if(__onAfterComposeObservable == null)
            {
                __onAfterComposeObservable = EventHorizonBlazorInterop.GetClass<Observable<EffectLayer>>(
                    this.___guid,
                    "onAfterComposeObservable",
                    (entity) =>
                    {
                        return new Observable<EffectLayer>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onAfterComposeObservable;
            }
            set
            {
__onAfterComposeObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onAfterComposeObservable",
                    value
                );
            }
        }

        private Observable<EffectLayer> __onSizeChangedObservable;
        public Observable<EffectLayer> onSizeChangedObservable
        {
            get
            {
            if(__onSizeChangedObservable == null)
            {
                __onSizeChangedObservable = EventHorizonBlazorInterop.GetClass<Observable<EffectLayer>>(
                    this.___guid,
                    "onSizeChangedObservable",
                    (entity) =>
                    {
                        return new Observable<EffectLayer>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onSizeChangedObservable;
            }
            set
            {
__onSizeChangedObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onSizeChangedObservable",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public EffectLayer() : base() { }

        public EffectLayer(
            ICachedEntity entity
        ) : base(entity)
        {
            ___guid = entity.___guid;
        }

        public EffectLayer(
            string name, Scene scene
        )
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "EffectLayer" },
                name, scene
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
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

        public CachedEntity serialize()
        {
            return EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "serialize" }
                }
            );
        }

        public void render()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "render" }
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

        public bool shouldRender()
        {
            return EventHorizonBlazorInterop.Func<bool>(
                new object[]
                {
                    new string[] { this.___guid, "shouldRender" }
                }
            );
        }

        public void dispose()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "dispose" }
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
        #endregion
    }
}