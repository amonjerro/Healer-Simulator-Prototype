using Prototype;

namespace Prototype.StateMachine
{
    public class SeekingState : AbsState<CharacterStates>
    {

        Character target;

        protected override void OnEnter()
        {
            // Set my target
        }

        protected override void OnExit()
        {
            // Attack if in range

        }

        protected override void OnUpdate()
        {
            // Update destination if necessary

            // Move towards destination

            // Check conditions
        }
    }
}