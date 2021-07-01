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

    
    
    [JsonConverter(typeof(CachedEntityConverter<PBRBaseSimpleMaterial>))]
    public class PBRBaseSimpleMaterial : PBRBaseMaterial
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods

        #endregion

        #region Accessors
        
        public bool doubleSided
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "doubleSided"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "doubleSided",
                    value
                );
            }
        }
        #endregion

        #region Properties
        
        public decimal maxSimultaneousLights
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "maxSimultaneousLights"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "maxSimultaneousLights",
                    value
                );
            }
        }

        
        public bool disableLighting
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "disableLighting"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "disableLighting",
                    value
                );
            }
        }

        private BaseTexture __environmentTexture;
        public BaseTexture environmentTexture
        {
            get
            {
            if(__environmentTexture == null)
            {
                __environmentTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "environmentTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __environmentTexture;
            }
            set
            {
__environmentTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "environmentTexture",
                    value
                );
            }
        }

        
        public bool invertNormalMapX
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "invertNormalMapX"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "invertNormalMapX",
                    value
                );
            }
        }

        
        public bool invertNormalMapY
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "invertNormalMapY"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "invertNormalMapY",
                    value
                );
            }
        }

        private BaseTexture __normalTexture;
        public BaseTexture normalTexture
        {
            get
            {
            if(__normalTexture == null)
            {
                __normalTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "normalTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __normalTexture;
            }
            set
            {
__normalTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "normalTexture",
                    value
                );
            }
        }

        private Color3 __emissiveColor;
        public Color3 emissiveColor
        {
            get
            {
            if(__emissiveColor == null)
            {
                __emissiveColor = EventHorizonBlazorInterop.GetClass<Color3>(
                    this.___guid,
                    "emissiveColor",
                    (entity) =>
                    {
                        return new Color3() { ___guid = entity.___guid };
                    }
                );
            }
            return __emissiveColor;
            }
            set
            {
__emissiveColor = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "emissiveColor",
                    value
                );
            }
        }

        private BaseTexture __emissiveTexture;
        public BaseTexture emissiveTexture
        {
            get
            {
            if(__emissiveTexture == null)
            {
                __emissiveTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "emissiveTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __emissiveTexture;
            }
            set
            {
__emissiveTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "emissiveTexture",
                    value
                );
            }
        }

        
        public decimal occlusionStrength
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "occlusionStrength"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "occlusionStrength",
                    value
                );
            }
        }

        private BaseTexture __occlusionTexture;
        public BaseTexture occlusionTexture
        {
            get
            {
            if(__occlusionTexture == null)
            {
                __occlusionTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "occlusionTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __occlusionTexture;
            }
            set
            {
__occlusionTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "occlusionTexture",
                    value
                );
            }
        }

        
        public decimal alphaCutOff
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "alphaCutOff"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "alphaCutOff",
                    value
                );
            }
        }

        private BaseTexture __lightmapTexture;
        public BaseTexture lightmapTexture
        {
            get
            {
            if(__lightmapTexture == null)
            {
                __lightmapTexture = EventHorizonBlazorInterop.GetClass<BaseTexture>(
                    this.___guid,
                    "lightmapTexture",
                    (entity) =>
                    {
                        return new BaseTexture() { ___guid = entity.___guid };
                    }
                );
            }
            return __lightmapTexture;
            }
            set
            {
__lightmapTexture = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "lightmapTexture",
                    value
                );
            }
        }

        
        public bool useLightmapAsShadowmap
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "useLightmapAsShadowmap"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "useLightmapAsShadowmap",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public PBRBaseSimpleMaterial() : base() { }

        public PBRBaseSimpleMaterial(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public PBRBaseSimpleMaterial(
            string name, Scene scene
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "PBRBaseSimpleMaterial" },
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
        #endregion
    }
}