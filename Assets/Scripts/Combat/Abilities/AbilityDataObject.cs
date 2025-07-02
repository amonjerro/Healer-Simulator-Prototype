using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Scriptable object that encapsulates all ability information
    /// </summary>
    [CreateAssetMenu(menuName = "Abilities/AbilityData")]
    class AbilityDataObject : ScriptableObject
    {
        [Tooltip("The icon that represents this skill visually")]
        public Sprite icon;

        [Tooltip("The type of effect this skill does")]
        public AbilityEffectType effectType;
        
        [Tooltip("The type of ability this is")]
        public AbilityType abilityType;

        [Tooltip("The target type of this skill")]
        public AbilityTargetType abilityTargetType;

        [Tooltip("Which side does this target, relative to the caster")]
        public ActorAttitude sideTargeted;

        [Tooltip("Does this apply a status effect")]
        public bool appliesStatus;

        [Tooltip("The identity and cost information for this ability")]
        public AbilityInformation abilityInformation;

        [Tooltip("The data for any status effect information for this ability")]
        public StatusEffectData statusEffectData;

        [Tooltip("The effect data information for this skill")]
        public AbilityEffectData effectData;

        [Tooltip("The animation duration for this skill")]
        public float animationDuration;
    }
}
