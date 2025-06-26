namespace Prototype.StateMachine
{
    public class CastingState : AbsPlayerAbilityState
    {
        GreaterThanCondition<float> timerExceeded;
        float timer;

        public CastingState()
        {
            timerExceeded = new GreaterThanCondition<float>(1.0f);
            Transition<AbilityStates> transition = new Transition<AbilityStates>();
            transition.SetCondition(timerExceeded);
            transitions.Add(AbilityStates.ChooseAbility, transition);
        }

        public override CharacterEvent GetAbilityEvent(InputKeys k)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnter()
        {
            timer = 0f;

        }

        protected override void OnExit()
        {
            Flush();
        }

        protected override void OnUpdate()
        {
            timer += TimeUtil.GetDelta();

            timerExceeded.SetValue(timer);
        }
    }
}