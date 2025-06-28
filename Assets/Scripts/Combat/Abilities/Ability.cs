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

    [Serializable]
    public class AbilityEffectData
    {
        public float healthFactor;
        public float confidenceFactor;
        public float stubbornessFactor;
    }

    [Serializable]
    public class AbilityInformation
    {
        public string abilityName;
        public int power;
        public int cost;
        public AudioClip audioClip;
    }

    public abstract class Ability
    {
        protected AbilityInformation abilityInformation;
        protected AbilityEffectData abilityData;
        protected StatusEffect statusEffect;
        protected abstract void AfterExecute();
        protected abstract void OnExecute();
        protected abstract void OnCancel();

        public void Execute()
        {
            OnExecute();
        }

        public void Resolve()
        {
            AfterExecute();
        }

        public void Cancel()
        {
            OnCancel();
        }
        
        public void SetStatusEffect(StatusEffectData effectData)
        {
            statusEffect = new StatusEffect(effectData);
        }

        public void SetInformation(AbilityInformation info)
        {
            abilityInformation = info;
        }

        public void SetEffectData(AbilityEffectData effectData) { 
            abilityData = effectData;
        }

        public int GetCost()
        {
            return abilityInformation.cost;
        }

        public AudioClip GetSoundClip()
        {
            return abilityInformation.audioClip;
        }

        protected void ApplyAbility(CharacterData data)
        {
            data.CurrentHealth += (int) abilityData.healthFactor;
            data.CurrentStubborness += (int) abilityData.stubbornessFactor;
            data.CurrentConfidence += (int)abilityData.confidenceFactor;
        }

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
            
        }

        public void SetTarget(Character target) { 
            this.target = target;
        }

        protected override void OnExecute()
        {
            System.Diagnostics.Debug.Assert(target != null);
            CharacterData data = target.GetCharacterData();
            System.Diagnostics.Debug.Assert(data != null);
            ApplyAbility(data);
            target.ResolveCombatEffect((int)abilityData.healthFactor);
        }

        protected override void AfterExecute()
        {
            UnityEngine.Debug.Log("Ability Executed");
        }
    }

}
