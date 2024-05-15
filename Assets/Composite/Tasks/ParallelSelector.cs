/**
 * Runs all tasks at the same time.
 * If one of the tasks is succed terminate others and return success.
 * If all of them failed returns failed.
 */

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Tasks
{
    public class ParallelSelector : CompositeIterator
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

            List<UniTask<CompositeResult>> compositeTasks 
                = new List<UniTask<CompositeResult>>();

            while(HasComposite())
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

                if (compositeResult == CompositeResult.Succeed)
                {
                    DisposeSubCancellationToken();
                    return SetState(CompositeResult.Succeed);
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            return SetState(CompositeResult.Failed);
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
