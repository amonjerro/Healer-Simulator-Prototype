using UnityEngine;

namespace Prototype
{
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