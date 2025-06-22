namespace Prototype
{
    class CombatCursor
    {
        CombatCursor() {
            CombatManager.TargetingAbilitySelected += StageAbility;
            CombatManager.CancelTargetingAbility += CursorCancel;
        }

        public void CleanUp()
        {
            CombatManager.TargetingAbilitySelected -= StageAbility;
            CombatManager.CancelTargetingAbility -= CursorCancel;
        }

        private void StageAbility(SingleTargetAbility ability)
        {

        }

        private void CursorCancel() { 
        
        }
    }
}
