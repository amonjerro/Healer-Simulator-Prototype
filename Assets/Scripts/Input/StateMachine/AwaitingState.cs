namespace Prototype.StateMachine
{
    
    /// <summary>
    /// State that awaits for input from the player
    /// </summary>
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

            CharacterEvent ev2 = new CharacterEvent<bool>(CharacterEventTypes.UIAbilityRequest, true);
            handler.PublishUIMessage(ev2);
        }

        protected override void OnExit()
        {
            // Stage the power
            CharacterEvent ev = new CharacterEvent<int>(CharacterEventTypes.SkillReady, (int)inputKey);
            handler.PublishMessage(ev);

            CharacterEvent ev2 = new CharacterEvent<bool>(CharacterEventTypes.UITargetRequest, true);
            handler.PublishUIMessage(ev2);


            // Flush stuff out
            inputKey = InputKeys.None;
            Flush();
        }

        protected override void OnUpdate()
        {
            targetingInputCondition.SetValue(IsAbilityInput(inputKey) && IsInputAvailable(inputKey));
        }

        /// <summary>
        /// Tests whether the input given corresponds to a given ability. 
        /// This is pretty hardcoded and should be informed by the player's loadout
        /// </summary>
        /// <param name="k">The input passed</param>
        /// <returns>True if it is an ability input</returns>
        private bool IsAbilityInput(InputKeys k)
        {
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

        private bool IsInputAvailable(InputKeys k)
        {
            return handler.CheckAbilityAvailability((int)inputKey);
        }

    }
}