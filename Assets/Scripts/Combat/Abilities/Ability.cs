using System;
using System.Diagnostics;
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
    }

    public class SingleTargetAbility : Ability
    {
        Character target;
        protected override void WillExecute()
        {
            CombatManager.TargetingAbilitySelected?.Invoke(this);
        }

        protected override void OnCancel()
        {
            CombatManager.CancelTargetingAbility?.Invoke();
        }

        public void SetTarget(Character target) { 
            this.target = target;
        }

        protected override void OnExecute()
        {
            System.Diagnostics.Debug.Assert(target != null);

            CharacterData data = target.GetCharacterData();

            System.Diagnostics.Debug.Assert(data != null);

            

        }

        protected override void AfterExecute()
        {
            UnityEngine.Debug.Log("Ability Executed");
        }
    }

}
