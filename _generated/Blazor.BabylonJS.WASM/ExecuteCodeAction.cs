/// Generated - Do Not Edit
namespace BABYLON
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;
    using Microsoft.JSInterop;

    
    
    [JsonConverter(typeof(CachedEntityConverter<ExecuteCodeAction>))]
    public class ExecuteCodeAction : Action
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

        #endregion
        
        #region Constructor
        public ExecuteCodeAction() : base() { }

        public ExecuteCodeAction(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public ExecuteCodeAction(
            object triggerOptions, ActionCallback<ActionEvent> func, Condition condition = null
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "ExecuteCodeAction" },
                triggerOptions, func, condition
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        #region func TODO: Get Comments as metadata identification
        private bool _isFuncEnabled = false;
        private readonly IDictionary<string, Func<Task>> _funcActionMap = new Dictionary<string, Func<Task>>();

        public string func(
            Func<Task> callback
        )
        {
            SetupFuncLoop();

            var handle = Guid.NewGuid().ToString();
            _funcActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool func_Remove(
            string handle
        )
        {
            return _funcActionMap.Remove(
                handle
            );
        }

        private void SetupFuncLoop()
        {
            if (_isFuncEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "func",
                "CallFuncActions",
                _invokableReference
            );
            _isFuncEnabled = true;
        }

        [JSInvokable]
        public async Task CallFuncActions()
        {
            foreach (var action in _funcActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        public void execute(ActionEvent evt)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "execute" }, evt
                }
            );
        }
        #endregion
    }
}