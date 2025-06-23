
using System;
using UnityEngine;

namespace Prototype
{

    public enum CharacterEventTypes
    {
        Movement,
        SkillUse,
        DamageTaken,
        Death
    }

    public class CharacterEvent
    {
        public CharacterEventTypes eventType;
        public float eventValue;

        public CharacterEvent(CharacterEventTypes eventType, float eventValue)
        {
            this.eventType = eventType;
            this.eventValue = eventValue;
        }
    }

    public class CharacterEventManager : MonoBehaviour
    {
        public static Action<CharacterEvent> onCharacterEvent;

        public static void BroadcastCharacterEvent(CharacterEvent characterEvent) { 
            onCharacterEvent?.Invoke(characterEvent);
        }
    }
}