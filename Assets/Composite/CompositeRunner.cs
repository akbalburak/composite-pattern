using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Attributes;
using Wave.Engine.Composite.Enums;
using Wave.Engine.Composite.Exception;

namespace Wave.Engine.Composite
{
    public abstract class CompositeRunner : CompositeObject
    {
        [SerializeField, ReadOnly]
        protected CompositeObject _compositeToRun;

        private CancellationTokenSource _cancellationTokenSource;

        protected virtual void Awake()
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                    continue;

                if (!child.TryGetComponent(out _compositeToRun))
                    continue;

                break;
            }
        }
        protected virtual void OnDestroy()
        {
            DisposeCancellationToken();
        }

        public async UniTask<CompositeResult> StartRunning()
        {
            if (CurrentState == CompositeResult.Running)
                throw new CompositeAlreadyRunningException(this);

            CancellationToken cancellationToken = GetCancellationToken();
            return await Run(cancellationToken);
        }
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            CompositeResult state = await _compositeToRun.Run(cancellationToken);
            SetState(state);

            if (state == CompositeResult.Failed)
                DisposeCancellationToken();

            return state;
        }
        public void StopRunning()
        {
            DisposeCancellationToken();
        }

        private CancellationToken GetCancellationToken()
        {
            DisposeCancellationToken();
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }
        private void DisposeCancellationToken()
        {
            if (_cancellationTokenSource == null)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

    }
}
