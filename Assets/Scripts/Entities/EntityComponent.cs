using UnityEngine;

namespace Entities
{
    public abstract class EntityComponent : MonoBehaviour
    {
        public abstract void Initialize(GameObject parent);
        public abstract void UpdateComponent();
    }
}