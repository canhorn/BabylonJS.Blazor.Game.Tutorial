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

    public interface IMaterialClearCoatDefines : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IMaterialClearCoatDefinesCachedEntity>))]
    public class IMaterialClearCoatDefinesCachedEntity : CachedEntityObject, IMaterialClearCoatDefines
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods

        #endregion

        #region Accessors

        #endregion

        #region Properties
        
        public bool CLEARCOAT
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_DEFAULTIOR
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_DEFAULTIOR"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_DEFAULTIOR",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_TEXTURE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_TEXTURE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TEXTURE",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_TEXTURE_ROUGHNESS
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESS"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESS",
                    value
                );
            }
        }

        
        public decimal CLEARCOAT_TEXTUREDIRECTUV
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "CLEARCOAT_TEXTUREDIRECTUV"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TEXTUREDIRECTUV",
                    value
                );
            }
        }

        
        public decimal CLEARCOAT_TEXTURE_ROUGHNESSDIRECTUV
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESSDIRECTUV"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESSDIRECTUV",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_BUMP
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_BUMP"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_BUMP",
                    value
                );
            }
        }

        
        public decimal CLEARCOAT_BUMPDIRECTUV
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "CLEARCOAT_BUMPDIRECTUV"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_BUMPDIRECTUV",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_USE_ROUGHNESS_FROM_MAINTEXTURE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_USE_ROUGHNESS_FROM_MAINTEXTURE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_USE_ROUGHNESS_FROM_MAINTEXTURE",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_TEXTURE_ROUGHNESS_IDENTICAL
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESS_IDENTICAL"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TEXTURE_ROUGHNESS_IDENTICAL",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_REMAP_F0
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_REMAP_F0"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_REMAP_F0",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_TINT
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_TINT"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TINT",
                    value
                );
            }
        }

        
        public bool CLEARCOAT_TINT_TEXTURE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "CLEARCOAT_TINT_TEXTURE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TINT_TEXTURE",
                    value
                );
            }
        }

        
        public decimal CLEARCOAT_TINT_TEXTUREDIRECTUV
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "CLEARCOAT_TINT_TEXTUREDIRECTUV"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "CLEARCOAT_TINT_TEXTUREDIRECTUV",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IMaterialClearCoatDefinesCachedEntity() : base() { }

        public IMaterialClearCoatDefinesCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}