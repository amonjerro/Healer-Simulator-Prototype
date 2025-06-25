using Prototype;

namespace Prototype.StateMachine
{
    public class AttackingState : AbsState<CharacterStates>
    {
        protected override void OnEnter()
        {
            // Perform the attack action

            // Begin a cooldown timer
        }

        protected override void OnExit()
        {
            // Flee if low on health

            // Insist if you're in range

            // Seek if you're not
        }

        protected override void OnUpdate()
        {
            // Check to see if the cooldown timer is over
        }
    }

}