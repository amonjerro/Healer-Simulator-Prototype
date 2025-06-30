namespace Prototype.StateMachine
{
    public class FindAttackTargetState : AICharacterState
    {
        EqualsCondition<bool> targetFound;
        Character c;

        public FindAttackTargetState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c) {
            stateValue = CharacterStates.FindTarget;
            targetFound = new EqualsCondition<bool>(true);
            Transition<CharacterStates> transition = new Transition<CharacterStates>();
            transition.SetCondition(targetFound);
            transitions.Add(CharacterStates.Seeking, transition);
        }
        protected override void OnEnter()
        {
            // Ready an attack ability
            CharacterEvent<int> characterEvent = new CharacterEvent<int>(CharacterEventTypes.SkillReady, 0);
            controller.PublishMessage(characterEvent);
            c = null;

            c = controller.FindTarget(false);
        }

        protected override void OnExit()
        {
            controller.SetTarget(c);
            CharacterEvent<Character> ev = new CharacterEvent<Character>(CharacterEventTypes.SetSkillTarget, c);
            controller.PublishMessage(ev);
        }

        protected override void OnUpdate()
        {
            targetFound.SetValue(c != null);
        }
    }
}