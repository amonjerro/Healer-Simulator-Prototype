namespace Prototype.StateMachine
{
    public class AwaitingState : AbsPlayerAbilityState
    {
        EqualsCondition<bool> targetingInputCondition;

        public AwaitingState()
        {
            targetingInputCondition = new EqualsCondition<bool>(true);
            Transition<AbilityStates> toTargeting = new Transition<AbilityStates>();
            toTargeting.SetCondition(targetingInputCondition);
            transitions.Add(AbilityStates.ChooseTarget, toTargeting);
        }

        protected override void OnEnter()
        {
            // Unstage powers
            
        }

        protected override void OnExit()
        {
            inputKey = InputKeys.None;

            // Stage the power
            Flush();
        }

        protected override void OnUpdate()
        {
            targetingInputCondition.SetValue(IsAbilityInput(inputKey));
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