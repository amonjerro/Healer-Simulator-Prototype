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

    public abstract class Ability
    {
        protected AbilityEffectData data;
        protected StatusEffect statusEffect;

        protected abstract void WillExecute();

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

        public void Prepare()
        {
            WillExecute();
        }

        public void Cancel()
        {
            OnCancel();
        }
        
        public void SetStatusEffect(StatusEffectData effectData)
        {
            statusEffect = new StatusEffect(effectData);
        }
    }

    public class SingleTargetAbility : Ability
    {
        Character target;
        protected override void WillExecute()
        {
            
        }

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

            UnityEngine.Debug.Log("Executing!");
            

        }

        protected override void AfterExecute()
        {
            UnityEngine.Debug.Log("Ability Executed");
        }
    }

}
