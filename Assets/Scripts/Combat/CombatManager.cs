using System;
using UnityEngine;

namespace Prototype
{

    /// <summary>
    /// Singleton manager class that controls the general flow of combat.
    /// </summary>
    public class CombatManager : MonoBehaviour
    {
        public static Action<SingleTargetAbility> TargetingAbilitySelected;
        public static Action CancelTargetingAbility;

        private void Awake()
        {
            TimeUtil.Initialize();
        }

        private void Update()
        {
            TimeUtil.UpdateDeltaTime();
        }
    }
}
