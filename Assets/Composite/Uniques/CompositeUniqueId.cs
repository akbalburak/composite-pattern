using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wave.Engine.Composite.Attributes;

namespace Wave.Engine.Interactables.Uniques
{
    [ExecuteInEditMode]
    public class CompositeUniqueId : MonoBehaviour
    {
        [SerializeField]
        private string _shortHash;

        [SerializeField, ReadOnly]
        private string _fullHash;

        private void Awake()
        {
            CheckIdUniqueness();
            AskChildrenToUpdateTheirFullHash();
        }
        private void OnValidate()
        {
            CheckIdUniqueness();
        }
        private void OnTransformParentChanged()
        {
            CheckIdUniqueness();
        }


        public string GetFullHash()
        {
            if (transform.parent == null)
                return _shortHash;

            if (!transform.parent.TryGetComponent(out CompositeUniqueId parentUniqueId))
            {
                parentUniqueId = transform.parent.gameObject.AddComponent<CompositeUniqueId>();
                parentUniqueId.CheckIdUniqueness();
            }

            return $"{parentUniqueId.GetFullHash()}_{_shortHash}";
        }


        private void CheckIdUniqueness()
        {
            if (Application.isPlaying)
                return;

            if (!SceneManager.GetActiveScene().IsValid())
                return;

            if (transform.parent == null)
            {
                CompositeUniqueId[] ids = FindObjectsByType<CompositeUniqueId>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID
                );

                int uniqueIdCount = ids.Count(x => string.Equals(x._fullHash, _shortHash));
                if (uniqueIdCount > 1 || string.IsNullOrEmpty(_shortHash))
                {
                    _shortHash = GenerateUniqueId();
                }
            }
            else
            {
                if (!transform.parent.TryGetComponent(out CompositeUniqueId parentUniqueId))
                    parentUniqueId = transform.parent.gameObject.AddComponent<CompositeUniqueId>();

                CompositeUniqueId[] children = parentUniqueId.GetComponentsInChildren<CompositeUniqueId>();

                int uniqueIdCount = children.Count(x => string.Equals(x._fullHash, _fullHash));
                if (uniqueIdCount > 1 || string.IsNullOrEmpty(_shortHash))
                {
                    _shortHash = GenerateUniqueId();
                }
            }

            _fullHash = GetFullHash();
        }
        private void AskChildrenToUpdateTheirFullHash()
        {
            CompositeUniqueId[] children = transform.GetComponentsInChildren<CompositeUniqueId>();
            foreach (var child in children)
                child.CheckIdUniqueness();
        }

        private string GenerateUniqueId()
        {
            return $"{System.Guid.NewGuid().ToString().Substring(0, 13)}";
        }

    }
}
