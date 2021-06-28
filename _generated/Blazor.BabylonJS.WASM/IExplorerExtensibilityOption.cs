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

    public interface IExplorerExtensibilityOption : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IExplorerExtensibilityOptionCachedEntity>))]
    public class IExplorerExtensibilityOptionCachedEntity : CachedEntityObject, IExplorerExtensibilityOption
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
        
        public string label
        {
            get
            {
            return EventHorizonBlazorInterop.Get<string>(
                    this.___guid,
                    "label"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "label",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IExplorerExtensibilityOptionCachedEntity() : base() { }

        public IExplorerExtensibilityOptionCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods
        #region action TODO: Get Comments as metadata identification
        private bool _isActionEnabled = false;
        private readonly IDictionary<string, Func<Task>> _actionActionMap = new Dictionary<string, Func<Task>>();

        public string action(
            Func<Task> callback
        )
        {
            SetupActionLoop();

            var handle = Guid.NewGuid().ToString();
            _actionActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool action_Remove(
            string handle
        )
        {
            return _actionActionMap.Remove(
                handle
            );
        }

        private void SetupActionLoop()
        {
            if (_isActionEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "action",
                "CallActionActions",
                _invokableReference
            );
            _isActionEnabled = true;
        }

        [JSInvokable]
        public async Task CallActionActions()
        {
            foreach (var action in _actionActionMap.Values)
            {
                await action();
            }
        }
        #endregion
        #endregion
    }
}