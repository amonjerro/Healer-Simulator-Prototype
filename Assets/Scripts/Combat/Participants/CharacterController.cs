using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Abstract class that defines the general behavior all character controllers must have
    /// </summary>
    public abstract class AbsCharacterController : MonoBehaviour
    {
        protected CharacterEventManager eventManager;
        protected abstract void SetupStateMachine();
        protected abstract void ProcessEvents(CharacterEvent ev);

        public void PublishMessage(CharacterEvent message)
        {
            eventManager.BroadcastCharacterEvent(message);
        }

    } 

}