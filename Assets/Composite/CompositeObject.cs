using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Attributes;
using Wave.Engine.Composite.Enums;
using Wave.Engine.Composite.Exception;

namespace Wave.Engine.Composite
{
    public abstract class CompositeObject : CompositeAutoNaming
    {
        public CompositeResult CurrentState => _currentState;

        [SerializeField, ReadOnly]
        private CompositeResult _currentState;

        private CancellationTokenRegistration? _runningCancellationRegistrationToken;

        public virtual async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            if (_currentState == CompositeResult.Running)
                throw new CompositeAlreadyRunningException(this);

            _runningCancellationRegistrationToken = cancellationToken.Register(OnCompositeTerminated);

            return SetState(CompositeResult.Running);
        }

        protected virtual void OnCompositeTerminated()
        {
            SetState(CompositeResult.Cancelled);
        }

        protected CompositeResult SetState(CompositeResult state)
        {
            if (state != CompositeResult.Running)
                DisposeRunningRegistrationToken();

            return _currentState = state;
        }

        private void DisposeRunningRegistrationToken()
        {
            if (!_runningCancellationRegistrationToken.HasValue)
                return;

            _runningCancellationRegistrationToken.Value.Dispose();
            _runningCancellationRegistrationToken = null;
        }
    }
}
