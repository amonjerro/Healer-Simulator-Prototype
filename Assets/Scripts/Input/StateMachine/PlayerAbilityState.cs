namespace Prototype.StateMachine
{
    public abstract class AbsPlayerAbilityState : AbsState<AbilityStates>
    {
        protected InputKeys inputKey;
        public void SetKey(InputKeys k)
        {
            inputKey = k;
        }
    }

}