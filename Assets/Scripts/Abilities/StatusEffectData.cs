using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "StatusEffect")]
    public class StatusEffectData : ScriptableObject
    {
        [Tooltip("The duration, in ticks, for this effect")]
        public int duration;

        [Tooltip("The effect this status will have on the character every tick")]
        public AbilityEffectData effectData;
    }
}