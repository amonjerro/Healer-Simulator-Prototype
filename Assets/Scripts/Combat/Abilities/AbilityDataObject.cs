using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Abilities/AbilityData")]
    class AbilityDataObject : ScriptableObject
    {
        public Sprite icon;
        public string abilityName;
        public int power;
        public AbilityEffectType effectType;
        public AbilityType abilityType;
        public AbilityTargetType abilityTargetType;
        public ActorAttitude sideTargeted;
        public bool appliesStatus;
        
        public StatusEffectData statusEffectData;
        public AbilityEffectData effectData;

        public float animationDuration;
    }
}
