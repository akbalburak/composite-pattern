/**
 * Run each task sequentially.
 * If one of the tasks return success, it selector return success.
 * If a task failure selector runs the next task.
 * If all the tasks failed selector returns failed state.
 */

using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Tasks
{
    public class SequenceSelector : CompositeIterator
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            ResetCompositeIterator();

            if (!HasComposite())
                return SetState(CompositeResult.Succeed);

            while (HasComposite())
            {
                CompositeObject composite = CurrentComposite();
                NextComposite();

                if (composite == null)
                    continue;
                
                CompositeResult compositeResult = await composite.Run(cancellationToken);
                
                if (cancellationToken.IsCancellationRequested) 
                    return SetState(CompositeResult.Cancelled);

                if (compositeResult == CompositeResult.Succeed)
                    return SetState(CompositeResult.Succeed);
            }

            return SetState(CompositeResult.Failed);
        }
    }
}
