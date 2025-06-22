using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Abilities/AbilityData")]
    class AbilityDataObject : ScriptableObject
    {
        public Sprite icon;
        public AbilityData data;
    }
}
