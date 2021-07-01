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

    public interface IMaterialBRDFDefines : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IMaterialBRDFDefinesCachedEntity>))]
    public class IMaterialBRDFDefinesCachedEntity : CachedEntityObject, IMaterialBRDFDefines
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
        
        public bool BRDF_V_HEIGHT_CORRELATED
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "BRDF_V_HEIGHT_CORRELATED"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "BRDF_V_HEIGHT_CORRELATED",
                    value
                );
            }
        }

        
        public bool MS_BRDF_ENERGY_CONSERVATION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "MS_BRDF_ENERGY_CONSERVATION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "MS_BRDF_ENERGY_CONSERVATION",
                    value
                );
            }
        }

        
        public bool SPHERICAL_HARMONICS
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SPHERICAL_HARMONICS"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SPHERICAL_HARMONICS",
                    value
                );
            }
        }

        
        public bool SPECULAR_GLOSSINESS_ENERGY_CONSERVATION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "SPECULAR_GLOSSINESS_ENERGY_CONSERVATION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "SPECULAR_GLOSSINESS_ENERGY_CONSERVATION",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IMaterialBRDFDefinesCachedEntity() : base() { }

        public IMaterialBRDFDefinesCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}