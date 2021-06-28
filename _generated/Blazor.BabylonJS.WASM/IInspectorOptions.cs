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

    public interface IInspectorOptions : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IInspectorOptionsCachedEntity>))]
    public class IInspectorOptionsCachedEntity : CachedEntityObject, IInspectorOptions
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
        
        public bool overlay
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "overlay"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "overlay",
                    value
                );
            }
        }

        
        public CachedEntity globalRoot
        {
            get
            {
            return EventHorizonBlazorInterop.Get<CachedEntity>(
                    this.___guid,
                    "globalRoot"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "globalRoot",
                    value
                );
            }
        }

        
        public bool showExplorer
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "showExplorer"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "showExplorer",
                    value
                );
            }
        }

        
        public bool showInspector
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "showInspector"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "showInspector",
                    value
                );
            }
        }

        
        public bool embedMode
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "embedMode"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "embedMode",
                    value
                );
            }
        }

        
        public bool handleResize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "handleResize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "handleResize",
                    value
                );
            }
        }

        
        public bool enablePopup
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "enablePopup"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "enablePopup",
                    value
                );
            }
        }

        
        public bool enableClose
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "enableClose"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "enableClose",
                    value
                );
            }
        }

        
        public IExplorerExtensibilityGroupCachedEntity[] explorerExtensibility
        {
            get
            {
            return EventHorizonBlazorInterop.GetArrayClass<IExplorerExtensibilityGroupCachedEntity>(
                    this.___guid,
                    "explorerExtensibility",
                    (entity) =>
                    {
                        return new IExplorerExtensibilityGroupCachedEntity() { ___guid = entity.___guid };
                    }
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "explorerExtensibility",
                    value
                );
            }
        }

        
        public string inspectorURL
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    this.___guid,
                    "inspectorURL"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "inspectorURL",
                    value
                );
            }
        }

        
        public int initialTab
        {
            get
            {
            return EventHorizonBlazorInterop.Get<int>(
                    this.___guid,
                    "initialTab"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "initialTab",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IInspectorOptionsCachedEntity() : base() { }

        public IInspectorOptionsCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}