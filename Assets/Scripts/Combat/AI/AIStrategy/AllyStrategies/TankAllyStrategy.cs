using Prototype.StateMachine;

namespace Prototype
{
    public class TankAllyStrategy : AbsAIStrategy
    {
        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            WanderState wanderState = new WanderState(machine, controller);
            IdleState idleState = new IdleState(machine, controller);

            wanderState.transitions[CharacterStates.Idle].TargetState = idleState;
            idleState.transitions[CharacterStates.Wandering].TargetState = wanderState;

            machine.SetStartingState(idleState);
            return machine;
        }
    }
}