using Prototype.StateMachine;

namespace Prototype
{
    public class SlimeStrategy : AbsAIStrategy
    {
        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            // State instantiation
            IdleState idleState = new IdleState(machine, controller);
            FindTargetState targetState = new FindTargetState(machine, controller);
            SeekingState seekingState = new SeekingState(machine, controller);
            AttackingState attackingState = new AttackingState(machine, controller);

            // Transition wiring
            idleState.transitions[CharacterStates.FindTarget].TargetState = targetState;
            targetState.transitions[CharacterStates.Seeking].TargetState = seekingState;
            seekingState.transitions[CharacterStates.Seeking].TargetState = seekingState;
            seekingState.transitions[CharacterStates.Attacking].TargetState= attackingState;
            attackingState.transitions[CharacterStates.Idle].TargetState = idleState;

            machine.SetStartingState(idleState);
            return machine;
        }
    }
}

