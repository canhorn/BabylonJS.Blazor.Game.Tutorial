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

    
    
    [JsonConverter(typeof(CachedEntityConverter<PBRBRDFConfiguration>))]
    public class PBRBRDFConfiguration : CachedEntityObject
    {
        #region Static Accessors

        #endregion

        #region Static Properties
        
        public static bool DEFAULT_USE_ENERGY_CONSERVATION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_ENERGY_CONSERVATION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_ENERGY_CONSERVATION",
                    value
                );
            }
        }

        
        public static bool DEFAULT_USE_SMITH_VISIBILITY_HEIGHT_CORRELATED
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SMITH_VISIBILITY_HEIGHT_CORRELATED"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SMITH_VISIBILITY_HEIGHT_CORRELATED",
                    value
                );
            }
        }

        
        public static bool DEFAULT_USE_SPHERICAL_HARMONICS
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SPHERICAL_HARMONICS"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SPHERICAL_HARMONICS",
                    value
                );
            }
        }

        
        public static bool DEFAULT_USE_SPECULAR_GLOSSINESS_INPUT_ENERGY_CONSERVATION
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SPECULAR_GLOSSINESS_INPUT_ENERGY_CONSERVATION"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    "BABYLON",
                    "PBRBRDFConfiguration.DEFAULT_USE_SPECULAR_GLOSSINESS_INPUT_ENERGY_CONSERVATION",
                    value
                );
            }
        }
        #endregion

        #region Static Methods

        #endregion

        #region Accessors

        #endregion

        #region Properties
        
        public bool useEnergyConservation
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "useEnergyConservation"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "useEnergyConservation",
                    value
                );
            }
        }

        
        public bool useSmithVisibilityHeightCorrelated
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "useSmithVisibilityHeightCorrelated"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "useSmithVisibilityHeightCorrelated",
                    value
                );
            }
        }

        
        public bool useSphericalHarmonics
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "useSphericalHarmonics"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "useSphericalHarmonics",
                    value
                );
            }
        }

        
        public bool useSpecularGlossinessInputEnergyConservation
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "useSpecularGlossinessInputEnergyConservation"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "useSpecularGlossinessInputEnergyConservation",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public PBRBRDFConfiguration() : base() { }

        public PBRBRDFConfiguration(
            ICachedEntity entity
        ) : base(entity)
        {
            ___guid = entity.___guid;
        }

        public PBRBRDFConfiguration(
            ActionCallback markAllSubMeshesAsMiscDirty
        )
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "PBRBRDFConfiguration" },
                markAllSubMeshesAsMiscDirty
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public void prepareDefines(IMaterialBRDFDefines defines)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "prepareDefines" }, defines
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

        public void copyTo(PBRBRDFConfiguration brdfConfiguration)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "copyTo" }, brdfConfiguration
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

        public void parse(object source, Scene scene, string rootUrl)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "parse" }, source, scene, rootUrl
                }
            );
        }
        #endregion
    }
}