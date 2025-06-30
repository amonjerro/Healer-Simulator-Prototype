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

        /// <summary>
        /// Set up the state machine for this character
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public abstract StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller);

        /// <summary>
        /// Determine this character's next target
        /// </summary>
        /// <param name="actorAttitude"></param>
        /// <returns></returns>
        public abstract Character FindNextTarget(ActorAttitude actorAttitude);
    }

    /// <summary>
    /// A very simple strategy that does nothing. Mostly for testing and debugging
    /// </summary>
    public class IdleStrategy : AbsAIStrategy
    {
        public IdleStrategy(AIController c) : base(c) { }

        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            IdleState idleState = new IdleState(machine, controller);
            idleState.transitions[CharacterStates.Wandering].TargetState = idleState;
            idleState.transitions[CharacterStates.FindTarget].TargetState = idleState;
            machine.SetStartingState(idleState);
            return machine;
        }

        public override Character FindNextTarget(ActorAttitude actorAttitude)
        {
            return null;
        }
    }

    /// <summary>
    /// Static strategy constructor, handles the creation of specific strategies
    /// </summary>
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