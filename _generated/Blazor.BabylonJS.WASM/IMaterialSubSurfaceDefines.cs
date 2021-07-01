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

    public interface IMaterialSubSurfaceDefines : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IMaterialSubSurfaceDefinesCachedEntity>))]
    public class IMaterialSubSurfaceDefinesCachedEntity : CachedEntityObject, IMaterialSubSurfaceDefines
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
        
        public bool SUBSURFACE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SUBSURFACE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SUBSURFACE",
                    value
                );
            }
        }

        
        public bool SS_REFRACTION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_REFRACTION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_REFRACTION",
                    value
                );
            }
        }

        
        public bool SS_TRANSLUCENCY
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_TRANSLUCENCY"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_TRANSLUCENCY",
                    value
                );
            }
        }

        
        public bool SS_SCATTERING
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_SCATTERING"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_SCATTERING",
                    value
                );
            }
        }

        
        public bool SS_THICKNESSANDMASK_TEXTURE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_THICKNESSANDMASK_TEXTURE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_THICKNESSANDMASK_TEXTURE",
                    value
                );
            }
        }

        
        public decimal SS_THICKNESSANDMASK_TEXTUREDIRECTUV
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "SS_THICKNESSANDMASK_TEXTUREDIRECTUV"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_THICKNESSANDMASK_TEXTUREDIRECTUV",
                    value
                );
            }
        }

        
        public bool SS_REFRACTIONMAP_3D
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_REFRACTIONMAP_3D"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_REFRACTIONMAP_3D",
                    value
                );
            }
        }

        
        public bool SS_REFRACTIONMAP_OPPOSITEZ
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_REFRACTIONMAP_OPPOSITEZ"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_REFRACTIONMAP_OPPOSITEZ",
                    value
                );
            }
        }

        
        public bool SS_LODINREFRACTIONALPHA
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_LODINREFRACTIONALPHA"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_LODINREFRACTIONALPHA",
                    value
                );
            }
        }

        
        public bool SS_GAMMAREFRACTION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_GAMMAREFRACTION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_GAMMAREFRACTION",
                    value
                );
            }
        }

        
        public bool SS_RGBDREFRACTION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_RGBDREFRACTION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_RGBDREFRACTION",
                    value
                );
            }
        }

        
        public bool SS_LINEARSPECULARREFRACTION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_LINEARSPECULARREFRACTION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_LINEARSPECULARREFRACTION",
                    value
                );
            }
        }

        
        public bool SS_LINKREFRACTIONTOTRANSPARENCY
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_LINKREFRACTIONTOTRANSPARENCY"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_LINKREFRACTIONTOTRANSPARENCY",
                    value
                );
            }
        }

        
        public bool SS_ALBEDOFORREFRACTIONTINT
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_ALBEDOFORREFRACTIONTINT"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_ALBEDOFORREFRACTIONTINT",
                    value
                );
            }
        }

        
        public bool SS_MASK_FROM_THICKNESS_TEXTURE
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_MASK_FROM_THICKNESS_TEXTURE"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_MASK_FROM_THICKNESS_TEXTURE",
                    value
                );
            }
        }

        
        public bool SS_MASK_FROM_THICKNESS_TEXTURE_GLTF
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SS_MASK_FROM_THICKNESS_TEXTURE_GLTF"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SS_MASK_FROM_THICKNESS_TEXTURE_GLTF",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IMaterialSubSurfaceDefinesCachedEntity() : base() { }

        public IMaterialSubSurfaceDefinesCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}