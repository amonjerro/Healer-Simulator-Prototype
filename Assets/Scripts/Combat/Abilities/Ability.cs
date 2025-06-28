using System;
using UnityEngine;

namespace Prototype
{
    public enum AbilityEffectType
    {
        Heal,
        Damage,
        Buff,
        Reposition
    }

    public enum AbilityType
    {
        Attack,
        Spell,
        VoiceCommand
    }

    public enum AbilityTargetType
    {
        SingleTarget,
        AllTargets
    }

    /// <summary>
    /// Class that encapsulates the data for an ability's effect
    /// </summary>
    [Serializable]
    public class AbilityEffectData
    {
        public float healthFactor;
        public float confidenceFactor;
        public float stubbornessFactor;
    }

    /// <summary>
    /// Class that encapsulates the data for an ability's information
    /// </summary>
    [Serializable]
    public class AbilityInformation
    {
        public string abilityName;
        public int power;
        public int cost;
        public AudioClip audioClip;
    }

    /// <summary>
    /// Abstract class representing an ability's behavior
    /// </summary>
    public abstract class Ability
    {
        protected AbilityInformation abilityInformation;
        protected AbilityEffectData abilityData;
        protected StatusEffect statusEffect;
        protected abstract void AfterExecute();
        protected abstract void OnExecute();
        protected abstract void OnCancel();

        /// <summary>
        /// Executes the ability and applies its effects
        /// </summary>
        public void Execute()
        {
            OnExecute();
        }

        /// <summary>
        /// Cleans up any remaining effects from an ability
        /// </summary>
        public void Resolve()
        {
            AfterExecute();
        }


        /// <summary>
        /// Executes any behavior associated with cancelling an ability
        /// </summary>
        public void Cancel()
        {
            OnCancel();
        }
        
        /// <summary>
        /// Sets the data for any status effect this ability applies
        /// </summary>
        /// <param name="effectData"></param>
        public void SetStatusEffect(StatusEffectData effectData)
        {
            statusEffect = new StatusEffect(effectData);
        }

        /// <summary>
        /// Sets the data for this ability's information
        /// </summary>
        /// <param name="info"></param>
        public void SetInformation(AbilityInformation info)
        {
            abilityInformation = info;
        }

        /// <summary>
        /// Sets the effect data for this ability
        /// </summary>
        /// <param name="effectData"></param>
        public void SetEffectData(AbilityEffectData effectData) { 
            abilityData = effectData;
        }

        /// <summary>
        /// Get the cost for this skill
        /// </summary>
        /// <returns>The mana cost for this skill</returns>
        public int GetCost()
        {
            return abilityInformation.cost;
        }

        /// <summary>
        /// Gets the sound clip associated with using this ability
        /// </summary>
        /// <returns>The audio clip for this ability</returns>
        public AudioClip GetSoundClip()
        {
            return abilityInformation.audioClip;
        }

        /// <summary>
        /// Applies the effects of this ability to a target character
        /// </summary>
        /// <param name="data"></param>
        protected void ApplyAbility(CharacterData data)
        {
            data.CurrentHealth += (int) abilityData.healthFactor;
            data.CurrentStubborness += (int) abilityData.stubbornessFactor;
            data.CurrentConfidence += (int)abilityData.confidenceFactor;
        }

        /// <summary>
        /// Apply this ability's status effects on a character
        /// </summary>
        /// <param name="character"></param>
        protected void ApplyStatusEffect(Character character)
        {
            character.AddStatusEffect(statusEffect);
        }
    }

    public class SingleTargetAbility : Ability
    {
        Character target;

        protected override void OnCancel()
        {
            // Currently unimplemented. Might delete this, tbh.   
        }

        /// <summary>
        /// Sets this ability's target
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Character target) { 
            this.target = target;
        }

        /// <summary>
        /// Obtains the data from this ability's target and applies this ability to it
        /// </summary>
        protected override void OnExecute()
        {
            System.Diagnostics.Debug.Assert(target != null);
            CharacterData data = target.GetCharacterData();
            System.Diagnostics.Debug.Assert(data != null);
            ApplyAbility(data);
            target.ResolveCombatEffect((int)abilityData.healthFactor);
        }

        // Right now this does nothing except debugging, so might delete later
        protected override void AfterExecute()
        {
            UnityEngine.Debug.Log("Ability Executed");
        }
    }

}
