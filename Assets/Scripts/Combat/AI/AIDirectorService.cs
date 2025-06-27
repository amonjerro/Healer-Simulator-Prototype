using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class AIDirectorService : MonoBehaviour
    {
        List<Character> hostileAIs;
        List<Character> FriendlyCharacters;
        Character playerCharacter;

        [SerializeField]
        float maxX;

        private void Awake()
        {
            hostileAIs = new List<Character>();
            FriendlyCharacters = new List<Character>();
        }

        public void RegisterActor(ActorAttitude attitude, Character controller)
        {
            switch (attitude)
            {
                case ActorAttitude.Friendly:
                    FriendlyCharacters.Add(controller);
                    EmitActorRegistered(controller);
                    return;
                case ActorAttitude.Player:
                    playerCharacter = controller;
                    FriendlyCharacters.Add(controller);
                    EmitActorRegistered(playerCharacter);
                    return;
                default:
                    hostileAIs.Add(controller);
                    return;
            }
        }

        private void EmitActorRegistered(Character controller) {
            ServiceMessage<Character> serviceMessage = new ServiceMessage<Character>(controller, ServiceMessageTypes.ActorRegistered);
            ServiceLocator.serviceAction?.Invoke(serviceMessage);
        }

        public float GetPlayerPosition()
        {
            return playerCharacter.gameObject.transform.position.x;
        }

        public float GetBounds()
        {
            return maxX;
        }

        public Character GetFriendlyCharacterByIndex(int index)
        {
            return FriendlyCharacters[index];
        }

        public Character GetHostileCharacterByIndex(int index)
        {
            return hostileAIs[index];
        }
    }
}