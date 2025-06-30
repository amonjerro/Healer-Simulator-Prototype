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
        protected AIController owner;

        public AbsAIStrategy(AIController owner)
        {
            this.owner = owner;
        }

        public abstract StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller);
        public abstract Character FindNextTarget(ActorAttitude actorAttitude);
    }

    public class IdleStrategy : AbsAIStrategy
    {
        public IdleStrategy(AIController c) : base(c) { }

        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            IdleState idleState = new IdleState(machine, controller);
            idleState.transitions[CharacterStates.Wandering].TargetState = idleState;
            machine.SetStartingState(idleState);
            return machine;
        }

        public override Character FindNextTarget(ActorAttitude actorAttitude)
        {
            return null;
        }
    }

    public static class AIStrategyFactory
    {
        public static AbsAIStrategy MakeStrategy(StrategyTypes t, AIController c)
        {
            switch (t)
            {
                case StrategyTypes.TankAlly:
                    return new TankAllyStrategy(c);
                case StrategyTypes.Enemy:
                    return new SlimeStrategy(c);
                default:
                    return new IdleStrategy(c);
            }
        }
    }
}