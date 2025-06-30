using Prototype.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SlimeStrategy : AbsAIStrategy
    {
        public SlimeStrategy(AIController c) : base(c) { }
        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            // State instantiation
            IdleState idleState = new IdleState(machine, controller);
            FindAttackTargetState targetState = new FindAttackTargetState(machine, controller);
            SeekingState seekingState = new SeekingState(machine, controller);
            AttackingState attackingState = new AttackingState(machine, controller);
            WanderState wanderingState = new WanderState(machine, controller);

            // Transition wiring
            idleState.transitions[CharacterStates.Wandering].TargetState = wanderingState;
            idleState.transitions[CharacterStates.FindTarget].TargetState = targetState;

            wanderingState.transitions[CharacterStates.Idle].TargetState = idleState;
            
            targetState.transitions[CharacterStates.Seeking].TargetState = seekingState;
            
            seekingState.transitions[CharacterStates.FindTarget].TargetState = targetState;
            seekingState.transitions[CharacterStates.Attacking].TargetState = attackingState;

            attackingState.transitions[CharacterStates.Idle].TargetState = idleState;

            machine.SetStartingState(idleState);
            return machine;
        }

        /// <summary>
        /// Finds the nearest enemy.
        /// </summary>
        /// <param name="toGet">Since the slime _is_ an enemy, it looks for candidates in the party roster</param>
        /// <returns>The selected party member</returns>
        public override Character FindNextTarget(ActorAttitude toGet)
        {
            AIDirectorService service = ServiceLocator.Instance.GetService<AIDirectorService>();
            List<Character> candidates = service.GetCharacterRoster(toGet);

            if (candidates.Count == 0) { return null; }

            float minDistance = 999;
            float dist;
            Character candidate = null;

            foreach(Character c in candidates)
            {
                dist = Mathf.Abs(c.transform.position.x - owner.transform.position.x);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    candidate = c;
                }
            }

            return candidate;
        }
    }
}

