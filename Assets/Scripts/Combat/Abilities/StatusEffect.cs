namespace Prototype
{
    public enum StatusEffects
    {
        Regeneration,
        Bleeding,
        Encouraged,
        Defiant
    }

    class StatusEffect
    {
        float expectedDuration;
        float elapsed;
        AbilityEffectData effectData;

        public StatusEffect(StatusEffectData data)
        {
            expectedDuration = data.duration;
            effectData = data.effectData;
        }

        public bool IsActive { get { return elapsed < expectedDuration; } }

        public void Update(CharacterData characterData)
        {
            elapsed += TimeUtil.deltaTime;

            if (!IsActive) {
                return;
            }

            characterData.CurrentHealth += (int)effectData.healthFactor;
            characterData.CurrentConfidence += (int)effectData.confidenceFactor;
            characterData.CurrentStubborness += (int)effectData.stubbornessFactor;
            
        }
    }

}