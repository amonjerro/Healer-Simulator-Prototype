using UnityEngine;

namespace Prototype.StateMachine
{
    /// <summary>
    /// A wander state to have an AI character move about between Idle cycles
    /// Makes characters feel a little more alive
    /// </summary>
    public class WanderState : AICharacterState
    {
        float wanderTarget;
        float tolerance = 0.3f;
        LessThanCondition<float> withinToleranceCondition;

        public WanderState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c)
        {

            stateValue = CharacterStates.Wandering;
            Transition<CharacterStates> toIdleTransition = new Transition<CharacterStates>();
            withinToleranceCondition = new LessThanCondition<float>(tolerance);
            toIdleTransition.SetCondition(withinToleranceCondition);
            transitions.Add(CharacterStates.Idle, toIdleTransition);

        }

        protected override void OnEnter()
        {
            // Set my target
            wanderTarget = controller.GetWanderDestination();
        }

        protected override void OnExit()
        {
            controller.MoveInDirection(0);
            // Attack if in range
            Flush();
        }

        protected override void OnUpdate()
        {
            // Update destination if necessary
            float distance = wanderTarget - controller.transform.position.x;
            controller.MoveInDirection(Mathf.Sign(distance));


            // Check conditions
            withinToleranceCondition.SetValue(Mathf.Abs(distance));
        }
    }
}