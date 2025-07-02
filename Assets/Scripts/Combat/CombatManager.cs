using NUnit.Framework;
using Prototype.UI;
using System;
using TMPro;
using UnityEngine;

namespace Prototype
{

    /// <summary>
    /// Singleton manager class that controls the general flow of combat.
    /// </summary>
    public class CombatManager : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("The game object that contains where the party's stats are going to show")]
        GameObject StatsContainer;

        [SerializeField]
        [Tooltip("The prefab object for a set of party member UI elements")]
        GameObject MemberStatsPrefab;

        [SerializeField]
        [Tooltip("The container for the player's powers")]
        PowerContainer PowerContainer;

        [SerializeField]
        [Tooltip("The prefabs for the party")]
        GameObject[] Party;

        [SerializeField]
        TextMeshProUGUI combatOverText;
        
        /*
         Unity lifecycle
         */
        private void Awake()
        {
            TimeUtil.Initialize();
            ServiceLocator.serviceAction += HandleServiceMessage;
        }

        private void Start()
        {
            for (int i = 0; i < Party.Length; i++)
            {
                GameObject member = Instantiate(Party[i]);
            }
        }

        private void Update()
        {
            TimeUtil.UpdateDeltaTime();
            TimeUtil.UpdateTick();
        }

        /// <summary>
        /// Create an instance of a party member's UI elements
        /// </summary>
        /// <param name="c"></param>
        private void CreateStatsUI(Character c)
        {
            // Create the stats UI and add it to the container
            GameObject instantiatedMemberStats = Instantiate(MemberStatsPrefab);
            instantiatedMemberStats.transform.SetParent(StatsContainer.transform);

            // Wire it to the corresponding character
            c.SetStatsUI(instantiatedMemberStats.GetComponent<MemberStats>());
        }

        /// <summary>
        /// Handle messages from other services
        /// </summary>
        /// <param name="message">The message received</param>
        private void HandleServiceMessage(ServiceMessage message)
        {
            switch (message.Type)
            {
                case ServiceMessageTypes.ActorRegistered:
                    CreateStatsUI(message.MessageValue as Character);
                    return;
                case ServiceMessageTypes.PlayerRegistered:
                    AssignPowersUI(message.MessageValue as Character);
                    return;
                case ServiceMessageTypes.GameWin:
                    UpdateCombatOverText("You win!");
                    return;
                case ServiceMessageTypes.GameLose:
                    UpdateCombatOverText("You lose!");
                    return;
                default:
                    return;
            }
        }
        
        /// <summary>
        /// Assigns the PowersUI to the player character
        /// </summary>
        /// <param name="c">A reference to the player character</param>
        private void AssignPowersUI(Character player)
        {
            player.SetPowersContainer(PowerContainer);
        }

        /// <summary>
        /// Show the game over text
        /// </summary>
        /// <param name="text">The message to show</param>
        public void UpdateCombatOverText(string text)
        {
            combatOverText.text = text;
        }
    }
}
