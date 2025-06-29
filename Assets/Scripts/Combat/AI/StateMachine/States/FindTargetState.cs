namespace Prototype.StateMachine
{
    public class FindTargetState : AICharacterState
    {
        EqualsCondition<bool> targetFound;
        Character c;

        public FindTargetState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c) { 
           
        }
        protected override void OnEnter()
        {
            // Ready an attack ability
            CharacterEvent<int> characterEvent = new CharacterEvent<int>(CharacterEventTypes.SkillReady, 0);
            controller.PublishMessage(characterEvent);
            c = null;
        }

        protected override void OnExit()
        {
            controller.SetTarget(c);
        }

        protected override void OnUpdate()
        {
            targetFound.SetValue(c != null);
        }
    }
}