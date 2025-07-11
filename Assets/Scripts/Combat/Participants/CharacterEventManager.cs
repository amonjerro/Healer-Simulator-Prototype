using System;
using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Class that handles communication between the different components within a character
    /// Keeps things nice and decoupled
    /// </summary>
    public class CharacterEventManager : MonoBehaviour
    {
        /*
            Message channels 
        */
        public Action<CharacterEvent> onCharacterEvent;
        public Action<CharacterEvent> onStatusChange;
        public static Action<CharacterEvent> onUIRequest;

        /*
         Broadcast functions
        */
        public void BroadcastCharacterEvent(CharacterEvent characterEvent)
        {
            onCharacterEvent?.Invoke(characterEvent);
        }

        public void BroadcastStatusChange(CharacterEvent characterEvent) { 
            onStatusChange?.Invoke(characterEvent);
        }

        public void BroadcastUIEvent(CharacterEvent characterEvent) { 
            onUIRequest?.Invoke(characterEvent);
        }
    }
}