using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Service class to orchestrate AI behavior and communication
    /// </summary>
    public class AIDirectorService : MonoBehaviour
    {
        // List of the hostile AIs in the scene
        List<Character> hostileAIs;
        // List of the friendly AIs in the scene. Includes the player as the 0th element.
        List<Character> FriendlyCharacters;
        // The player characters
        Character playerCharacter;

        [SerializeField]
        [Tooltip("The x-axis bounds of the game space")]
        float maxX;

        private void Awake()
        {
            hostileAIs = new List<Character>();
            FriendlyCharacters = new List<Character>();
        }

        /// <summary>
        /// Registers an AI actor in one of the corresponding lists.
        /// </summary>
        /// <param name="attitude">Determines which list to register this actor in</param>
        /// <param name="controller">The Character controller to register</param>
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

        /// <summary>
        /// Emit a message to other services stating an actor has been registered.
        /// </summary>
        /// <param name="controller">Indicates the actor registered</param>
        private void EmitActorRegistered(Character controller) {
            ServiceMessage<Character> serviceMessage = new ServiceMessage<Character>(controller, ServiceMessageTypes.ActorRegistered);
            ServiceLocator.serviceAction?.Invoke(serviceMessage);
        }

        /// <summary>
        /// Expose the player's position to AIs
        /// </summary>
        /// <returns>The x location for the player</returns>
        public float GetPlayerPosition()
        {
            return playerCharacter.gameObject.transform.position.x;
        }

        /// <summary>
        /// Get the playspace bounds
        /// </summary>
        /// <returns>The max x position in screen space. Negate it to get the lower bound</returns>
        public float GetBounds()
        {
            return maxX;
        }

        /// <summary>
        /// Gets a character controller from the friendly list by its index
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The character controller found at that index</returns>
        public Character GetFriendlyCharacterByIndex(int index)
        {
            return FriendlyCharacters[index];
        }

        /// <summary>
        /// Gets a character controller from the hostile list by its index
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The character controller found at that index</returns>
        public Character GetHostileCharacterByIndex(int index)
        {
            return hostileAIs[index];
        }

        /// <summary>
        /// Gets the active character roster for either side
        /// </summary>
        /// <param name="actorAttitude">The attitude of the characters to get</param>
        /// <returns>A roster of characters</returns>
        public List<Character> GetCharacterRoster(ActorAttitude actorAttitude)
        {
            if (actorAttitude == ActorAttitude.Friendly) {
                return FriendlyCharacters;
            } else
            {
                return hostileAIs;
            }
        }
    }
}