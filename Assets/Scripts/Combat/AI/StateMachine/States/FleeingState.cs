using Prototype;

namespace Prototype.StateMachine
{
    public class FleeingState : AbsState<CharacterStates>
    {
        protected override void OnEnter()
        {
            // Get a reference to the player
        }

        protected override void OnExit()
        {
            // Seek an enemy
        }

        protected override void OnUpdate()
        {
            // Try to move left of the player
        }
    }
}