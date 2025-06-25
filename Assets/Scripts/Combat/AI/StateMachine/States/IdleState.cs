using Prototype;
using UnityEngine;

namespace Prototype.StateMachine
{
    public class IdleState : AICharacterState
    {
        float timeToIdle = 2.0f;
        float timer;
        GreaterThanCondition<float> condition;
        public IdleState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c) {
            stateValue = CharacterStates.Idle;
            condition = new GreaterThanCondition<float>(timeToIdle);
            Transition<CharacterStates> transition = new Transition<CharacterStates>();
            transition.SetCondition(condition);
            transitions.Add(CharacterStates.Wandering, transition);
            
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
            // Do nothing for a sec
            timer += TimeUtil.GetDelta();

            condition.SetValue(timer);
        }
    }
}