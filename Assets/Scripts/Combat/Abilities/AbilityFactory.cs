namespace Prototype
{
    internal class AbilityBuilder
    {
        public Ability Ability { get; set; }
        public AbilityBuilder ComputeEffectData(AbilityDataObject dataObject)
        {
            AbilityEffectData computedData = new AbilityEffectData();
            computedData.confidenceFactor =  dataObject.effectData.confidenceFactor * dataObject.abilityInformation.power;
            computedData.stubbornessFactor =  dataObject.effectData.stubbornessFactor * dataObject.abilityInformation.power;
            computedData.healthFactor = dataObject.effectData.healthFactor * dataObject.abilityInformation.power;
            Ability.SetEffectData(computedData);
            return this;
        }

        public AbilityBuilder AddStatusEffectData(AbilityDataObject dataObject)
        {
            if (dataObject.appliesStatus)
            {
                Ability.SetStatusEffect(dataObject.statusEffectData);
            }
            return this;
        }

        public AbilityBuilder AddInformation(AbilityDataObject dataObject)
        {
            Ability.SetInformation(dataObject.abilityInformation);
            return this;
        }

    }

    internal static class AbilityFactory
    {
        public static Ability MakeAbility(AbilityDataObject dataObject)
        {
            AbilityBuilder builder = new AbilityBuilder();
            switch (dataObject.abilityTargetType) {
                case AbilityTargetType.SingleTarget:
                    Ability ab = new SingleTargetAbility();
                    builder.Ability = ab;
                    return builder.ComputeEffectData(dataObject).AddStatusEffectData(dataObject).AddInformation(dataObject).Ability;
                // To do: implement the case below
                default:
                    return null;
            }
        }

        
    }
}