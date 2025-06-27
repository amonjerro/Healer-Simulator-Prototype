using Prototype.UI;
using System;
using UnityEngine;

namespace Prototype
{

    /// <summary>
    /// Singleton manager class that controls the general flow of combat.
    /// </summary>
    public class CombatManager : MonoBehaviour
    {


        [SerializeField]
        GameObject StatsContainer;

        [SerializeField]
        GameObject MemberStatsPrefab;

        [SerializeField]
        GameObject[] Party;

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
        }

        private void CreateStatsUI(Character c)
        {
            // Create the stats UI and add it to the container
            GameObject instantiatedMemberStats = Instantiate(MemberStatsPrefab);
            instantiatedMemberStats.transform.SetParent(StatsContainer.transform);

            // Wire it to the corresponding character
            c.SetStatsUI(instantiatedMemberStats.GetComponent<MemberStats>());
        }

        private void HandleServiceMessage(ServiceMessage message)
        {
            if (message.Type == ServiceMessageTypes.ActorRegistered)
            {
                CreateStatsUI(message.MessageValue as Character);
            }
        }
    }
}
