using System.Collections.Generic;
using Prototype.StateMachine;

namespace Prototype
{
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

        public override Character FindNextTarget(ActorAttitude toGet)
        {
            AIDirectorService service = ServiceLocator.Instance.GetService<AIDirectorService>();
            List<Character> candidates = service.GetCharacterRoster(toGet);

            if (candidates.Count == 0) { return null; }

            return candidates[0];
        }
    }
}