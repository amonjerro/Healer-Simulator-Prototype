using System;
using UnityEngine;

namespace Prototype
{
    public class CharacterEventManager : MonoBehaviour
    {
        public Action<CharacterEvent> onCharacterEvent;

        private void Start()
        {

        }

        public void BroadcastCharacterEvent(CharacterEvent characterEvent)
        {
            onCharacterEvent?.Invoke(characterEvent);
        }
    }
}