using System.Collections.Generic;
using Prototype.StateMachine;

namespace Prototype
{
    public class TankAllyStrategy : AbsAIStrategy
    {
        public TankAllyStrategy(AIController c) : base(c) { }
        public override StateMachine<CharacterStates> SetupAIStateMachine(StateMachine<CharacterStates> machine, AIController controller)
        {
            WanderState wanderState = new WanderState(machine, controller);
            IdleState idleState = new IdleState(machine, controller);

            wanderState.transitions[CharacterStates.Idle].TargetState = idleState;
            idleState.transitions[CharacterStates.Wandering].TargetState = wanderState;

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