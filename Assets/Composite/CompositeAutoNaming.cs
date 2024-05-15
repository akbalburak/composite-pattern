using UnityEngine;
using Wave.Engine.Composite.Extends;

namespace Wave.Engine.Composite
{
    public abstract class CompositeAutoNaming : MonoBehaviour
    {
        [SerializeField]
        private bool _autoNameComposite = true;

        public virtual void OnValidate()
        {
            if (Application.isPlaying)
                return;

            if (!_autoNameComposite)
                return;

            string name = GetCompositeName();
            if (string.IsNullOrEmpty(name))
                return;

            transform.name = name;
        }

        protected virtual string GetCompositeName()
        {
            return $"{NamingExtension.SpaceBeforeUpper(this.GetType().Name)}";
        }
    }
}
