namespace Prototype.StateMachine
{
    public class TargetingState : AbsPlayerAbilityState
    {
        EqualsCondition<bool> cancelCondition;
        EqualsCondition<bool> targetChosenCondition;
        public TargetingState()
        {
            cancelCondition = new EqualsCondition<bool>(true);
            targetChosenCondition = new EqualsCondition<bool>(true);

            Transition<AbilityStates> cancelTransition = new Transition<AbilityStates>();
            Transition<AbilityStates> targetChosenTransition = new Transition<AbilityStates>();

            cancelTransition.SetCondition(cancelCondition);
            targetChosenTransition.SetCondition(targetChosenCondition);

            transitions.Add(AbilityStates.UsingAbility, targetChosenTransition);
            transitions.Add(AbilityStates.ChooseAbility, cancelTransition);
        }

        protected override void OnEnter()
        {
            inputKey = InputKeys.None;
        }

        protected override void OnExit()
        {
            Flush();
        }

        protected override void OnUpdate()
        {
            cancelCondition.SetValue(IsCancelInput(inputKey));
            targetChosenCondition.SetValue(IsTargetingInput(inputKey));
        }

        private bool IsCancelInput(InputKeys k)
        {
            return k == InputKeys.Cancel;
        }

        private bool IsTargetingInput(InputKeys k) {
            switch (k)
            {
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