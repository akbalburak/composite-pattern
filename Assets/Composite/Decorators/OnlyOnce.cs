/*
 * Save the state of the child composite object and return that state in next call.
 */

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;
using Wave.Engine.Interactables.Uniques;

namespace Wave.Engine.Composite.Decorators
{
    [RequireComponent(typeof(CompositeUniqueId))]
    public class OnlyOnce : CompositeOutputObject
    {
        public bool SaveOnSuccess = true;
        public bool SaveOnFailure = false;

        private CompositeUniqueId _unique;
        private string _uniqueId;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (_unique == null)
            {
                _unique = GetComponent<CompositeUniqueId>();
                _uniqueId = CompsoiteIdManager.Instance.GetUniqueId($"OO_{_unique}");
            }

            int saveState = PlayerPrefs.GetInt(_uniqueId, 0);
            if (saveState != 0)
            {
                CompositeResult result = (CompositeResult)saveState;
                return base.SetState(result);
            }

            CompositeResult newState = await _composite.Run(cancellationToken);
            switch (newState)
            {
                case CompositeResult.Succeed:
                    if (SaveOnSuccess)
                        PlayerPrefs.SetInt(_uniqueId, (int)CompositeResult.Succeed);
                    break;
                case CompositeResult.Failed:
                    if (SaveOnFailure)
                        PlayerPrefs.SetInt(_uniqueId, (int)CompositeResult.Failed);
                    break;
            }

            return base.SetState(newState);
        }

        protected override string GetCompositeName()
        {
            return $"[ONLY ONCE]";
        }
    }
}
