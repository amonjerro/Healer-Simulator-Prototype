using System;

namespace Prototype
{
    static class CombatManager
    {
        public static Action<SingleTargetAbility> TargetingAbilitySelected;
        public static Action CancelTargetingAbility;
    }
}
