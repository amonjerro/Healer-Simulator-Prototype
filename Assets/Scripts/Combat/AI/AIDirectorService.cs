using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class AIDirectorService : MonoBehaviour
    {
        List<Character> hostileAIs;
        List<Character> friendlyAIs;
        Character playerCharacter;

        [SerializeField]
        float maxX;

        private void Awake()
        {
            hostileAIs = new List<Character>();
            friendlyAIs = new List<Character>();
        }

        public void RegisterActor(ActorAttitude attitude, Character controller)
        {
            switch (attitude)
            {
                case ActorAttitude.Friendly:
                    friendlyAIs.Add(controller);
                    return;
                case ActorAttitude.Player:
                    playerCharacter = controller;
                    return;
                default:
                    hostileAIs.Add(controller);
                    return;
            }
        }

        public float GetPlayerPosition()
        {
            return playerCharacter.gameObject.transform.position.x;
        }

        public float GetBounds()
        {
            return maxX;
        }
    }
}