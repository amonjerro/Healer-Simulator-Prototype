namespace Prototype.StateMachine
{
    public class DeathState : AICharacterState
    {
        public DeathState(StateMachine<CharacterStates> s, AIController c) : base(s, c) { 
            stateValue = CharacterStates.Dead;
        }

        protected override void OnEnter()
        {
            controller.HandleDeath();  
        }

        protected override void OnExit()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }
    }
}