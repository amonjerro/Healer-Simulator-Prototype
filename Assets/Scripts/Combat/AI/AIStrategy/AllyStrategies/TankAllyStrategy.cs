using System.Collections.Generic;
using Prototype.StateMachine;

namespace Prototype
{
    /// <summary>
    /// AI function execution strategy for the Tank Ally
    /// </summary>
    public class TankAllyStrategy : AbsAIStrategy
    {
        public TankAllyStrategy(AIController c) : base(c) { }

        
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
            wanderingState.transitions[CharacterStates.Idle].TargetState = idleState;
            idleState.transitions[CharacterStates.FindTarget].TargetState = targetState;
            targetState.transitions[CharacterStates.Seeking].TargetState = seekingState;
            seekingState.transitions[CharacterStates.FindTarget].TargetState = targetState;
            seekingState.transitions[CharacterStates.Attacking].TargetState = attackingState;
            attackingState.transitions[CharacterStates.Idle].TargetState = idleState;

            machine.SetStartingState(idleState);
            return machine;
        }

        /// <summary>
        /// Current implementation is for the tank to search for the first enemy that spawned.
        /// This can be changed to protect the healer more.
        /// </summary>
        /// <param name="toGet">The side to get. For the tank it's mostly gonna be enemies</param>
        /// <returns>The character to target</returns>
        public override Character FindNextTarget(ActorAttitude toGet)
        {
            AIDirectorService service = ServiceLocator.Instance.GetService<AIDirectorService>();
            List<Character> candidates = service.GetCharacterRoster(toGet);

            if (candidates.Count == 0) { return null; }

            return candidates[0];
        }
    }
}