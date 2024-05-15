/**
 * Runs all tasks at the same time.
 * Wait for all of them to returns success.
 * If one of the tasks failed, terminate the other tasks and return parallel is failed.
 */

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Tasks
{
    public class Parallel : CompositeIterator
    {
        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy()
        {
            DisposeSubCancellationToken();
        }

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            ResetCompositeIterator();

            if (!HasComposite())
                return SetState(CompositeResult.Succeed);

            CancellationToken subCancellationToken = GetCancellationToken();

            List<UniTask<CompositeResult>> compositeTasks = new List<UniTask<CompositeResult>>();

            while (HasComposite())
            {
                CompositeObject composite = CurrentComposite();
                NextComposite();

                if (composite == null)
                    continue;

                compositeTasks.Add(composite.Run(subCancellationToken));
            }

            while (compositeTasks.Count > 0)
            {
                int taskIndex = compositeTasks.FindIndex(x => x.Status != UniTaskStatus.Pending);
                if (taskIndex == -1)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, subCancellationToken);
                    continue;
                }

                CompositeResult compositeResult = await compositeTasks[taskIndex];
                compositeTasks.RemoveAt(taskIndex);

                if (subCancellationToken.IsCancellationRequested)
                    return SetState(CompositeResult.Cancelled);

                if (compositeResult == CompositeResult.Failed)
                {
                    DisposeSubCancellationToken();
                    return SetState(CompositeResult.Failed);
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            return SetState(CompositeResult.Succeed);
        }

        protected override void OnCompositeTerminated()
        {
            base.OnCompositeTerminated();
            DisposeSubCancellationToken();
        }

        private CancellationToken GetCancellationToken()
        {
            DisposeSubCancellationToken();
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }
        private void DisposeSubCancellationToken()
        {
            if (_cancellationTokenSource == null)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
