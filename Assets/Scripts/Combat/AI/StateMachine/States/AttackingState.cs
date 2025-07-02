using Prototype;

namespace Prototype.StateMachine
{
    /// <summary>
    /// Class that determines what to do when a character is attacking.
    /// </summary>
    public class AttackingState : AICharacterState
    {
        EqualsCondition<bool> cond;
        public AttackingState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c)
        {
            stateValue = CharacterStates.Attacking;
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