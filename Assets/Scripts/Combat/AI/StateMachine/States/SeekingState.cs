using Unity.VisualScripting;
using UnityEngine;

namespace Prototype.StateMachine
{
    public class SeekingState : AICharacterState
    {

        Character target;
        float seekTimer = 2;
        float elapsed;
        float range = 0.5f;
        EqualsCondition<bool> targetDead;
        GreaterThanCondition<float> seekExceeded;
        LessThanCondition<float> withinRange;

        public SeekingState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c)
        {
            stateValue = CharacterStates.Seeking;
            targetDead = new EqualsCondition<bool>(true);
            seekExceeded = new GreaterThanCondition<float>(seekTimer);
            withinRange = new LessThanCondition<float>(range);
            OrCondition orCondition = new OrCondition(seekExceeded, targetDead);
            Transition<CharacterStates> toFindingTargetTransition = new Transition<CharacterStates>();
            Transition<CharacterStates> toAttackingTransition = new Transition<CharacterStates>();

            toFindingTargetTransition.SetCondition(orCondition);
            toAttackingTransition.SetCondition(withinRange);

            transitions.Add(CharacterStates.Attacking, toAttackingTransition);
            transitions.Add(CharacterStates.FindTarget, toFindingTargetTransition);
        }

        protected override void OnEnter()
        {
            // Set my target
            target = controller.GetTarget();
            elapsed = 0;
        }

        protected override void OnExit()
        {
            controller.MoveInDirection(0);
            Flush();
        }

        protected override void OnUpdate()
        {
            elapsed += TimeUtil.GetDelta();

            // Update destination if necessary
            float distance = target.transform.position.x - controller.transform.position.x;
            // Move towards destination
            controller.MoveInDirection(Mathf.Sign(distance));

            // Check conditions
            targetDead.SetValue(!target.IsAlive());
            seekExceeded.SetValue(elapsed);
            withinRange.SetValue(Mathf.Abs(distance));

        }
    }
}