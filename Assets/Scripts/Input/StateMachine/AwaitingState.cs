using UnityEditor.Playables;
using UnityEngine;

namespace Prototype.StateMachine
{
    public class AwaitingState : AbsPlayerAbilityState
    {
        EqualsCondition<bool> targetingInputCondition;

        public AwaitingState() : base()
        {
            stateValue = AbilityStates.ChooseAbility;
            targetingInputCondition = new EqualsCondition<bool>(true);
            Transition<AbilityStates> toTargeting = new Transition<AbilityStates>();
            toTargeting.SetCondition(targetingInputCondition);
            transitions.Add(AbilityStates.ChooseTarget, toTargeting);
        } 

        protected override void OnEnter()
        {
            // Unstage powers
            CharacterEvent ev = new CharacterEvent<bool>(CharacterEventTypes.SkillUse, false);
            handler.PublishMessage(ev);
        }

        protected override void OnExit()
        {
            // Stage the power
            CharacterEvent ev = new CharacterEvent<int>(CharacterEventTypes.SkillReady, (int) inputKey);
            handler.PublishMessage(ev);


            // Flush stuff out
            inputKey = InputKeys.None;
            Flush();
        }

        protected override void OnUpdate()
        {
            targetingInputCondition.SetValue(IsAbilityInput(inputKey));
        }
        public override CharacterEvent GetAbilityEvent(InputKeys k)
        {
            throw new System.NotImplementedException();
        }

        private bool IsAbilityInput(InputKeys k)
        {
            switch (k) {
                case InputKeys.One:
                case InputKeys.Two:
                case InputKeys.Three:
                    return true;
                default:
                    return false;
            }
        }

    }
}