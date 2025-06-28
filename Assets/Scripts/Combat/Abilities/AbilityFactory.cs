namespace Prototype
{
    /// <summary>
    /// Builder pattern class to build Ability objects from its saved data
    /// </summary>
    internal class AbilityBuilder
    {
        public Ability Ability { get; set; }
        
        /// <summary>
        /// Uses an ability's specification information to compute its effect. 
        /// Does not take caster into account.
        /// </summary>
        /// <param name="dataObject">The data specs of this ability</param>
        /// <returns>The builder, to allow for build chaining</returns>
        public AbilityBuilder ComputeEffectData(AbilityDataObject dataObject)
        {
            AbilityEffectData computedData = new AbilityEffectData();
            computedData.confidenceFactor =  dataObject.effectData.confidenceFactor * dataObject.abilityInformation.power;
            computedData.stubbornessFactor =  dataObject.effectData.stubbornessFactor * dataObject.abilityInformation.power;
            computedData.healthFactor = dataObject.effectData.healthFactor * dataObject.abilityInformation.power;
            Ability.SetEffectData(computedData);
            return this;
        }


        /// <summary>
        /// Adds status effect information to the built ability
        /// </summary>
        /// <param name="dataObject">The data specs of this ability</param>
        /// <returns>The builder, to allow for build chanining</returns>
        public AbilityBuilder AddStatusEffectData(AbilityDataObject dataObject)
        {
            if (dataObject.appliesStatus)
            {
                Ability.SetStatusEffect(dataObject.statusEffectData);
            }
            return this;
        }

        /// <summary>
        /// Adds the ability information to the built Ability object
        /// </summary>
        /// <param name="dataObject">The data specs of this ability</param>
        /// <returns>The builder, to allow for build chanining</returns>
        public AbilityBuilder AddInformation(AbilityDataObject dataObject)
        {
            Ability.SetInformation(dataObject.abilityInformation);
            return this;
        }

    }

    /// <summary>
    /// Factory pattern object that leverages the builder to return Ability objects
    /// </summary>
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