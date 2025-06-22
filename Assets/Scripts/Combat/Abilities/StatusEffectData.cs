using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "StatusEffect")]
    public class StatusEffectData : ScriptableObject
    {
        public float duration;
        public AbilityEffectData effectData;
    }
}