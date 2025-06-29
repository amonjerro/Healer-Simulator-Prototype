using Prototype;

namespace Prototype.StateMachine
{
    public class AttackingState : AICharacterState
    {
        EqualsCondition<bool> cond;
        public AttackingState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c) {

            cond = new EqualsCondition<bool>(true);
            Transition<CharacterStates> transition = new Transition<CharacterStates>();
            transition.SetCondition(cond);
            transitions.Add(CharacterStates.Idle, transition);
        }

        protected override void OnEnter()
        {
            // Perform the attack action
            CharacterEvent<bool> ce = new CharacterEvent<bool>(CharacterEventTypes.SkillUse, true);
            controller.PublishMessage(ce);

        }

        protected override void OnExit()
        {
            Flush();
        }

        protected override void OnUpdate()
        {
            cond.SetValue(true);
        }
    }

}