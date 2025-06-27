namespace Prototype.StateMachine
{
    public abstract class AbsPlayerAbilityState : AbsState<AbilityStates>
    {
        protected InputKeys inputKey;
        protected PlayerAbilityHandler handler;

        protected AbsPlayerAbilityState()
        {
            transitions = new System.Collections.Generic.Dictionary<AbilityStates, Transition<AbilityStates>>();
        }
        
        public void SetKey(InputKeys k)
        {
            inputKey = k;
        }

        public void SetAbilityHandler(PlayerAbilityHandler handler)
        {
            this.handler = handler;
        }
    }

}