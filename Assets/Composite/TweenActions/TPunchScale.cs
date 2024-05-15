using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.TweenActions
{
    public class TPunchScale : CompositeObject
    {
        public Transform Target;
        public Vector3 PunchScale = new Vector3(.25f, .25f, .25f);
        public float Duration = .25f;
        public bool WaitTillComplete;

        private Tween _punchScaleTween;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            _punchScaleTween?.Kill(complete: true);

            _punchScaleTween = Target.DOPunchScale(PunchScale, Duration, 1);

            if (WaitTillComplete)
                await _punchScaleTween.WithCancellation(cancellationToken);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            if (WaitTillComplete)
                return $"DO Punch Scale {Target.name} for {Duration}s Seconds and Wait for Complete";

            return $"DO Punch Scale {Target.name} For {Duration}s Seconds";
        }
    }
}
