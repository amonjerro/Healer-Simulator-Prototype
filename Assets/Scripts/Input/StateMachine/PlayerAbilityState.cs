namespace Prototype.StateMachine
{
    public abstract class AbsPlayerAbilityState : AbsState<AbilityStates>
    {
        protected InputKeys inputKey;

        public abstract CharacterEvent GetAbilityEvent(InputKeys k);
    }

}