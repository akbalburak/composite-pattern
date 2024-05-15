using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.TweenActions
{
    public class TMove : CompositeObject
    {
        public Transform Target;
        public bool LocalPosition;
        public Vector3 Position;
        public float Duration = .5f;
        public bool WaitTillComplete;
        public Ease Ease = Ease.Linear;
        public UpdateType UpdateType = UpdateType.Normal;

        private Tween _doMoveTween;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            _doMoveTween?.Kill(complete: false);

            if (LocalPosition)
            {
                _doMoveTween = Target.DOLocalMove(Position, Duration)
                    .SetEase(Ease)
                    .SetUpdate(UpdateType);
            }
            else
            {
                _doMoveTween = Target.DOLocalMove(Position, Duration)
                  .SetEase(Ease)
                  .SetUpdate(UpdateType);
            }

            if (WaitTillComplete)
                await _doMoveTween.WithCancellation(cancellationToken);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            if (WaitTillComplete)
                return $"DO Move {Target.name} For {Duration}s Seconds and Wait for Complete";

            return $"DO Move {Target.name} For {Duration}s Seconds";
        }
    }
}
