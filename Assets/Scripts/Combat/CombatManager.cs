using System;
using UnityEngine;

namespace Prototype
{
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
