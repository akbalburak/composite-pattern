/**
 * Loop the given composite as much as the given amount.
 */

using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Decorators
{
    public class Repeater : CompositeOutputObject
    {
        public bool StopInFailure;
        public bool StopInSuccess;
        public int RepeatCount = -1;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            while (RepeatCount > 0 || RepeatCount == -1)
            {
                if (RepeatCount > 0)
                    RepeatCount--;

                CompositeResult compositeResult = await _composite.Run(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    return SetState(CompositeResult.Cancelled);

                if (StopInFailure)
                {
                    if (compositeResult is CompositeResult.Failed)
                    {
                        return SetState(CompositeResult.Failed);
                    }
                }

                if (StopInSuccess)
                {
                    if (compositeResult is CompositeResult.Succeed)
                    {
                        return SetState(CompositeResult.Succeed);
                    }
                }

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            return SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (RepeatCount == -1)
                return $"[REPEATER] INFINITE Times";
            return $"[REPEATER] {RepeatCount} Times";
        }
    }
}
