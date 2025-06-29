using Prototype.StateMachine;

namespace Prototype
{
    public enum StrategyTypes
    {
        Idle,
        TankAlly,
        DPSAlly,
        Enemy
    }

    public abstract class AbsAIStrategy
    {
        public abstract StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller);
    }

    public class IdleStrategy : AbsAIStrategy
    {
        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            IdleState idleState = new IdleState(machine, controller);
            idleState.transitions[CharacterStates.Wandering].TargetState = idleState;
            machine.SetStartingState(idleState);
            return machine;
        }
    }

    public static class AIStrategyFactory
    {
        public static AbsAIStrategy MakeStrategy(StrategyTypes t)
        {
            switch (t)
            {
                case StrategyTypes.TankAlly:
                    return new TankAllyStrategy();
                default:
                    return new IdleStrategy();
            }
        }
    }
}