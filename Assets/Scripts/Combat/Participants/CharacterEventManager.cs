using System;
using UnityEngine;

namespace Prototype
{
    public class CharacterEventManager : MonoBehaviour
    {
        public Action<CharacterEvent> onCharacterEvent;
        public Action<CharacterEvent> onStatusChange;

        public void BroadcastCharacterEvent(CharacterEvent characterEvent)
        {
            onCharacterEvent?.Invoke(characterEvent);
        }

        public void BroadcastStatusChange(CharacterEvent characterEvent) { 
            onStatusChange?.Invoke(characterEvent);
        }
    }
}