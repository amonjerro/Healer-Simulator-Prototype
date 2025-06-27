namespace Prototype
{
    internal static class AbilityFactory
    {
        public static Ability MakeAbility(AbilityDataObject dataObject)
        {
            switch (dataObject.abilityTargetType) {
                case AbilityTargetType.SingleTarget:
                    Ability ab = new SingleTargetAbility();
                    if (dataObject.appliesStatus)
                    {
                        ab = AddStatusEffectData(dataObject, ab);
                    }
                    return ab;
                // To do: implement the case below
                default:
                    return null;
            }
        }

        public static Ability AddStatusEffectData(AbilityDataObject dataObject, Ability ability) {
            ability.SetStatusEffect(dataObject.statusEffectData);
            return ability;
        }
    }
}