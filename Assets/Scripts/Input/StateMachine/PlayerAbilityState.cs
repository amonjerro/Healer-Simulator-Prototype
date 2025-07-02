namespace Prototype.StateMachine
{
    /// <summary>
    /// The abstract class that governs the states that make up the player's ability targeting pipeline
    /// </summary>
    public abstract class AbsPlayerAbilityState : AbsState<AbilityStates>
    {
        protected InputKeys inputKey;
        protected PlayerCharacterController handler;

        protected AbsPlayerAbilityState()
        {
            transitions = new System.Collections.Generic.Dictionary<AbilityStates, Transition<AbilityStates>>();
        }

        public void SetKey(InputKeys k)
        {
            inputKey = k;
        }

        public void SetAbilityHandler(PlayerCharacterController handler)
        {
            this.handler = handler;
        }
    }

}