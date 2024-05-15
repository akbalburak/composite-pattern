/**
 * Run each composite task one by one.
 * If all succeed return success.
 * If one of the task is failed sequence returns failed.
 * When failed, no longor keep running rest of the tasks.
 */

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Tasks
{
    public class Sequence : CompositeIterator
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            ResetCompositeIterator();

            if (!HasComposite())
                return SetState(CompositeResult.Succeed);

            while(HasComposite())
            {
                CompositeObject composite = CurrentComposite();
                NextComposite();

                if (composite == null)
                    continue;

                CompositeResult compositeResult = await composite.Run(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    return SetState(CompositeResult.Cancelled);

                if (compositeResult == CompositeResult.Failed)
                    return SetState(CompositeResult.Failed);
            }

            return SetState(CompositeResult.Succeed);
        }
    }
}
