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

    public interface IExplorerExtensibilityGroup : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter<IExplorerExtensibilityGroupCachedEntity>))]
    public class IExplorerExtensibilityGroupCachedEntity : CachedEntityObject, IExplorerExtensibilityGroup
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
        
        public IExplorerExtensibilityOptionCachedEntity[] entries
        {
            get
            {
            return EventHorizonBlazorInterop.GetArrayClass<IExplorerExtensibilityOptionCachedEntity>(
                    this.___guid,
                    "entries",
                    (entity) =>
                    {
                        return new IExplorerExtensibilityOptionCachedEntity() { ___guid = entity.___guid };
                    }
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "entries",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IExplorerExtensibilityGroupCachedEntity() : base() { }

        public IExplorerExtensibilityGroupCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods
        #region predicate TODO: Get Comments as metadata identification
        private bool _isPredicateEnabled = false;
        private readonly IDictionary<string, Func<Task>> _predicateActionMap = new Dictionary<string, Func<Task>>();

        public string predicate(
            Func<Task> callback
        )
        {
            SetupPredicateLoop();

            var handle = Guid.NewGuid().ToString();
            _predicateActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool predicate_Remove(
            string handle
        )
        {
            return _predicateActionMap.Remove(
                handle
            );
        }

        private void SetupPredicateLoop()
        {
            if (_isPredicateEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "predicate",
                "CallPredicateActions",
                _invokableReference
            );
            _isPredicateEnabled = true;
        }

        [JSInvokable]
        public async Task CallPredicateActions()
        {
            foreach (var action in _predicateActionMap.Values)
            {
                await action();
            }
        }
        #endregion
        #endregion
    }
}