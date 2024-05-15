using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.TweenActions
{
    public class TRotate : CompositeObject
    {
        public Transform Target;
        public bool LocalRotation;
        public Vector3 Rotation;
        public float Duration = .5f;
        public bool WaitTillComplete;
        public Ease Ease = Ease.Linear;
        public UpdateType UpdateType = UpdateType.Normal;

        private Tween _doRotateTween;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            _doRotateTween?.Kill(complete: false);

            if (LocalRotation)
            {
                _doRotateTween = Target.DOLocalRotate(Rotation, Duration)
                    .SetEase(Ease)
                    .SetUpdate(UpdateType);
            }
            else
            {
                _doRotateTween = Target.DORotate(Rotation, Duration)
                    .SetEase(Ease)
                    .SetUpdate(UpdateType);
            }

            if (WaitTillComplete)
                await _doRotateTween.WithCancellation(cancellationToken);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            if (WaitTillComplete)
                return $"DO Rotate {Target.name} For {Duration}s Seconds and Wait for Complete";

            return $"DO Rotate {Target.name} For {Duration}s Seconds.";
        }
    }
}
