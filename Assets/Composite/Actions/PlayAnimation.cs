using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class PlayAnimation : CompositeObject
    {
        public Animator Animator;
        public int LayerIndex;
        public string StateName;
        public bool FailIfNotFound;
        public bool WaitComplete;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            int stateHash = Animator.StringToHash(StateName);

            if (!Animator.HasState(LayerIndex, stateHash))
            {
                if (FailIfNotFound)
                    return base.SetState(CompositeResult.Failed);
                else
                    return base.SetState(CompositeResult.Succeed);
            }

            Animator.Play(stateHash, LayerIndex, 0);

            if (WaitComplete)
            {
                await UniTask.WaitUntil(() =>
                {
                    AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);
                    return int.Equals(stateHash, state.shortNameHash) && state.normalizedTime >= .99f;
                }, cancellationToken: cancellationToken);
            }

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Animator == null)
                return base.GetCompositeName();

            string animatorName = Animator.name;
            if (Animator.transform.parent != null)
                animatorName = $"{Animator.transform.parent.name}/{Animator.name} Animator";

            if (WaitComplete)
                return $"Play {StateName} Animation in {animatorName} and Wait Complete";

            return $"Play {StateName} Animation in {animatorName}";
        }
    }
}
