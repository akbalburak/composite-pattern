using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class PlaySound : CompositeObject
    {
        public AudioClip AudioClip;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            //AudioBus.CallSoundPlay(AudioClip);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (AudioClip == null)
                return base.GetCompositeName();

            return $"Play {AudioClip.name} sound";
        }
    }
}
