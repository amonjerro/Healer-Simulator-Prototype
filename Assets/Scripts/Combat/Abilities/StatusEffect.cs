namespace Prototype
{
    public enum StatusEffects
    {
        Regeneration,   // Healz over time
        Bleeding,       // Damage over time
        Encouraged,     // Confidence augment over time
        Defiant         // Stubborness augment over time
    }

    /// <summary>
    /// Class that encapsulates behavior for status effects
    /// </summary>
    public class StatusEffect
    {

        int expectedDuration; // Duration in ticks this effect is supposed to last
        int elapsed; // How long has elapsed since this status effect was set
        AbilityEffectData effectData; // The effect this status effect will cause every tick

        public StatusEffect(StatusEffectData data)
        {
            expectedDuration = data.duration;
            effectData = data.effectData;
            elapsed = 0;
        }

        public bool IsActive { get { return elapsed < expectedDuration; } }

        public void Update(CharacterData characterData)
        {
            elapsed++;

            if (!IsActive) {
                return;
            }

            characterData.CurrentHealth += (int)effectData.healthFactor;
            characterData.CurrentConfidence += (int)effectData.confidenceFactor;
            characterData.CurrentStubborness += (int)effectData.stubbornessFactor;
            
        }
    }

}