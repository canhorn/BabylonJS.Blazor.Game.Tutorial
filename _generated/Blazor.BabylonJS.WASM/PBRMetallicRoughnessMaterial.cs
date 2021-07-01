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

    
    
    [JsonConverter(typeof(CachedEntityConverter<PBRMetallicRoughnessMaterial>))]
    public class PBRMetallicRoughnessMaterial : PBRBaseSimpleMaterial
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static PBRMetallicRoughnessMaterial Parse(object source, Scene scene, string rootUrl)
        {
            return EventHorizonBlazorInterop.FuncClass<PBRMetallicRoughnessMaterial>(
                entity => new PBRMetallicRoughnessMaterial() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { "BABYLON", "PBRMetallicRoughnessMaterial", "Parse" }, source, scene, rootUrl
                }
            );
        }
        #endregion

        #region Accessors

        #endregion

        #region Properties
        private Color3 __baseColor;
        public Color3 baseColor
        {
            get
            {
            if(__baseColor == null)
            {
                __baseColor = EventHorizonBlazorInterop.GetClass<Color3>(
                    this.___guid,
                    "baseColor",
                    (entity) =>
                    {
                        return new Color3() { ___guid = entity.___guid };
                    }
                );
            }
            return __baseColor;
            }
            set
            {
__baseColor = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "baseColor",
                    value
                );
            }
        }

        private BaseTexture __baseTexture;
        public BaseTexture baseTexture
        {
            get
            {
            if(__baseTexture == null)
            {
                __baseTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "baseTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __baseTexture;
            }
            set
            {
__baseTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "baseTexture",
                    value
                );
            }
        }

        
        public decimal metallic
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "metallic"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "metallic",
                    value
                );
            }
        }

        
        public decimal roughness
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "roughness"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "roughness",
                    value
                );
            }
        }

        private BaseTexture __metallicRoughnessTexture;
        public BaseTexture metallicRoughnessTexture
        {
            get
            {
            if(__metallicRoughnessTexture == null)
            {
                __metallicRoughnessTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "metallicRoughnessTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __metallicRoughnessTexture;
            }
            set
            {
__metallicRoughnessTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "metallicRoughnessTexture",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public PBRMetallicRoughnessMaterial() : base() { }

        public PBRMetallicRoughnessMaterial(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public PBRMetallicRoughnessMaterial(
            string name, Scene scene
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "PBRMetallicRoughnessMaterial" },
                name, scene
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

        public PBRMetallicRoughnessMaterial clone(string name)
        {
            return EventHorizonBlazorInterop.FuncClass<PBRMetallicRoughnessMaterial>(
                entity => new PBRMetallicRoughnessMaterial() { ___guid = entity.___guid },
                new object[]
                {
                    new string[] { this.___guid, "clone" }, name
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