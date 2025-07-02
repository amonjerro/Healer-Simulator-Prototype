namespace Prototype.StateMachine
{
    /// <summary>
    /// State the player is in while casting
    /// For the most part it's just a pause state
    /// </summary>
    public class CastingState : AbsPlayerAbilityState
    {
        float timer;
        GreaterThanCondition<float> timerCondition;

        public CastingState() : base()
        {
            stateValue = AbilityStates.UsingAbility;
            timerCondition = new GreaterThanCondition<float>(0);
            Transition<AbilityStates> transition = new Transition<AbilityStates>();
            transition.SetCondition(timerCondition);
            transitions.Add(AbilityStates.ChooseAbility, transition);

        }

        protected override void OnEnter()
        {
            CharacterEvent ev = new CharacterEvent<bool>(CharacterEventTypes.SkillUse, true);
            handler.PublishMessage(ev);
            timer = 0f;
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {
            timer += TimeUtil.GetDelta();
            timerCondition.SetValue(timer);
        }
    }
}