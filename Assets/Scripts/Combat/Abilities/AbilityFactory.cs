namespace Prototype
{
    internal static class AbilityFactory
    {
        public static Ability MakeAbility(AbilityDataObject dataObject)
        {
            switch (dataObject.abilityTargetType) {
                case AbilityTargetType.SingleTarget:
                    Ability ab = new SingleTargetAbility();
                    ab = ComputeEffectData(dataObject, ab);
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

        public static Ability ComputeEffectData(AbilityDataObject dataObject, Ability ability) {
            AbilityEffectData computedData = dataObject.effectData;
            computedData.confidenceFactor *= dataObject.power;
            computedData.stubbornessFactor *= dataObject.power;
            computedData.healthFactor *= dataObject.power;
            ability.SetEffectData(computedData);
            return ability;
        }

        public static Ability AddStatusEffectData(AbilityDataObject dataObject, Ability ability) {
            ability.SetStatusEffect(dataObject.statusEffectData);
            return ability;
        }
    }
}