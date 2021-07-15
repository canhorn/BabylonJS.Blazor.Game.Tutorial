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

    
    
    [JsonConverter(typeof(CachedEntityConverter<CubeTexture>))]
    public class CubeTexture : BaseTexture
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static CubeTexture CreateFromImages(string[] files, Scene scene, System.Nullable<bool> noMipmap = null)
        {
            return EventHorizonBlazorInterop.FuncClass<CubeTexture>(
                entity => new CubeTexture() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "CubeTexture", "CreateFromImages" }, files, scene, noMipmap
                }
            );
        }

        public static CubeTexture CreateFromPrefilteredData(string url, Scene scene, object forcedExtension = null, System.Nullable<bool> createPolynomials = null)
        {
            return EventHorizonBlazorInterop.FuncClass<CubeTexture>(
                entity => new CubeTexture() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "CubeTexture", "CreateFromPrefilteredData" }, url, scene, forcedExtension, createPolynomials
                }
            );
        }

        public static CubeTexture Parse(object parsedTexture, Scene scene, string rootUrl)
        {
            return EventHorizonBlazorInterop.FuncClass<CubeTexture>(
                entity => new CubeTexture() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "CubeTexture", "Parse" }, parsedTexture, scene, rootUrl
                }
            );
        }
        #endregion

        #region Accessors
        private Vector3 __boundingBoxSize;
        public Vector3 boundingBoxSize
        {
            get
            {
            if(__boundingBoxSize == null)
            {
                __boundingBoxSize = EventHorizonBlazorInterop.GetClass<Vector3>(
                    this.___guid,
                    "boundingBoxSize",
                    (entity) =>
                    {
                        return new Vector3() { ___guid = entity.___guid };
                    }
                );
            }
            return __boundingBoxSize;
            }
            set
            {
__boundingBoxSize = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "boundingBoxSize",
                    value
                );
            }
        }

        
        public decimal rotationY
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "rotationY"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "rotationY",
                    value
                );
            }
        }

        
        public bool noMipmap
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "noMipmap"
                );
            }
        }
        #endregion

        #region Properties
        private Observable<CubeTexture> __onLoadObservable;
        public Observable<CubeTexture> onLoadObservable
        {
            get
            {
            if(__onLoadObservable == null)
            {
                __onLoadObservable = EventHorizonBlazorInterop.GetClass<Observable<CubeTexture>>(
                    this.___guid,
                    "onLoadObservable",
                    (entity) =>
                    {
                        return new Observable<CubeTexture>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onLoadObservable;
            }
            set
            {
__onLoadObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onLoadObservable",
                    value
                );
            }
        }

        
        public string url
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    this.___guid,
                    "url"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "url",
                    value
                );
            }
        }

        private Vector3 __boundingBoxPosition;
        public Vector3 boundingBoxPosition
        {
            get
            {
            if(__boundingBoxPosition == null)
            {
                __boundingBoxPosition = EventHorizonBlazorInterop.GetClass<Vector3>(
                    this.___guid,
                    "boundingBoxPosition",
                    (entity) =>
                    {
                        return new Vector3() { ___guid = entity.___guid };
                    }
                );
            }
            return __boundingBoxPosition;
            }
            set
            {
__boundingBoxPosition = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "boundingBoxPosition",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public CubeTexture() : base() { }

        public CubeTexture(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public CubeTexture(
            string rootUrl, Scene sceneOrEngine, string[] extensions = null, System.Nullable<bool> noMipmap = null, string[] files = null, ActionCallback onLoad = null, ActionCallback<string, CachedEntity> onError = null, System.Nullable<decimal> format = null, System.Nullable<bool> prefiltered = null, object forcedExtension = null, System.Nullable<bool> createPolynomials = null, System.Nullable<decimal> lodScale = null, System.Nullable<decimal> lodOffset = null, object loaderOptions = null
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "CubeTexture" },
                rootUrl, sceneOrEngine, extensions, noMipmap, files, onLoad, onError, format, prefiltered, forcedExtension, createPolynomials, lodScale, lodOffset, loaderOptions
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public string getClassName()
        {
            return EventHorizonBlazorInterop.Func<string>(
                new object[]
                {
                    new string[] { this.___guid, "getClassName" }
                }
            );
        }

        public void updateURL(string url, string forcedExtension = null, ActionCallback onLoad = null, System.Nullable<bool> prefiltered = null)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "updateURL" }, url, forcedExtension, onLoad, prefiltered
                }
            );
        }

        public void delayLoad(string forcedExtension = null)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "delayLoad" }, forcedExtension
                }
            );
        }

        public Matrix getReflectionTextureMatrix()
        {
            return EventHorizonBlazorInterop.FuncClass<Matrix>(
                entity => new Matrix() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { this.___guid, "getReflectionTextureMatrix" }
                }
            );
        }

        public void setReflectionTextureMatrix(Matrix value)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "setReflectionTextureMatrix" }, value
                }
            );
        }

        public CubeTexture clone()
        {
            return EventHorizonBlazorInterop.FuncClass<CubeTexture>(
                entity => new CubeTexture() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { this.___guid, "clone" }
                }
            );
        }
        #endregion
    }
}