using System;
using System.Diagnostics;

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
    public class AbilityData
    {
        public string name;
        public AbilityEffectType effectType;
        public AbilityType abilityType;
        public int power;
        public float healthFactor;
        public float confidenceFactor;
        public float stubbornessFactor;
    }

    public abstract class Ability
    {
        protected AbilityData data;

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

    public abstract class SingleTargetAbility : Ability
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
            Debug.Assert(target != null);

            CharacterData data = target.GetCharacterData();

            Debug.Assert(data != null);


        }
    }

}
