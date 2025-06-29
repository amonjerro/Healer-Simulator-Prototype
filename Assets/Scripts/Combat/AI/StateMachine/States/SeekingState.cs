using UnityEngine;

namespace Prototype.StateMachine
{
    public class SeekingState : AICharacterState
    {

        Character target;
        float range = 0.2f;
        EqualsCondition<bool> targetDead;
        LessThanCondition<float> withinRange;

        public SeekingState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c)
        {
            targetDead = new EqualsCondition<bool>(true);
            withinRange = new LessThanCondition<float>(range);
            Transition<CharacterStates> toFindingTargetTransition = new Transition<CharacterStates>();
            Transition<CharacterStates> toAttackingTransition = new Transition<CharacterStates>();

            toFindingTargetTransition.SetCondition(targetDead);
            toAttackingTransition.SetCondition(withinRange);

            transitions.Add(CharacterStates.Attacking, toAttackingTransition);
            transitions.Add(CharacterStates.FindTarget, toFindingTargetTransition);
        }

        protected override void OnEnter()
        {
            // Set my target
            target = controller.GetTarget();
        }

        protected override void OnExit()
        {
            controller.MoveInDirection(0);
            Flush();
        }

        protected override void OnUpdate()
        {
            // Update destination if necessary
            float distance = target.transform.position.x - controller.transform.position.x;
            
            // Move towards destination
            controller.MoveInDirection(Mathf.Sign(distance));

            // Check conditions
            targetDead.SetValue(!target.IsAlive());
            withinRange.SetValue(distance);

        }
    }
}