using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Abilities/AbilityData")]
    class AbilityDataObject : ScriptableObject
    {
        public Sprite icon;
        public AbilityEffectType effectType;
        public AbilityType abilityType;
        public AbilityTargetType abilityTargetType;
        public ActorAttitude sideTargeted;
        public bool appliesStatus;
        
        public AbilityInformation abilityInformation;
        public StatusEffectData statusEffectData;
        public AbilityEffectData effectData;

        public float animationDuration;
    }
}
