using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// A character's personality which will govern how they respond to effects on their stats
    /// Still unimplemented
    /// </summary>
    public enum Personality
    {
        Neutral, // Nothing special
        Nervous, // Loses confidence quickly
        Confident, // Gains confidence quickly
        Stubborn, // Gains stubborness quickly
        Compliant // Loses stubborness quickly
    }

    /// <summary>
    /// A character's stats
    /// </summary>
    public class CharacterData
    {
        public int maxHealth;
        public int maxMana;
        private int _currentMana;
        private int _manaRegen = 2;
        private int _currentHealth;
        private int _currentStubborness;
        private int _currentConfidence;
        public float stubbornessBurnRate;
        public float confidenceMapFactor;

        /// <summary>
        /// Health property
        /// </summary>
        public int CurrentHealth
        {
            get { return _currentHealth; }

            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        /// <summary>
        /// Convert ratio of health values to float
        /// </summary>
        /// <returns>A float indicating the ratio of current to max</returns>
        public float GetFloatHealth()
        {
            return CurrentHealth / (float)maxHealth;
        }

        /// <summary>
        /// Confidence property. Should normally be invisible to the player, mostly governs AI beahvior
        /// </summary>
        public int CurrentConfidence
        {
            get { return _currentConfidence; }

            set { _currentConfidence = Mathf.Clamp(value, 0, CombatConstants.MaxConfidence); }
        }

        /// <summary>
        /// Stubborness property
        /// </summary>
        public int CurrentStubborness
        {
            get { return _currentStubborness; }
            set { _currentStubborness = Mathf.Clamp(value, 0, CombatConstants.MaxStubborness); }
        }

        /// <summary>
        /// Converts ratio of stubborness to float
        /// </summary>
        /// <returns>A float indicating the ratio of current to max</returns>
        public float GetFloatStubborn()
        {
            return CurrentStubborness / (float)CombatConstants.MaxStubborness;
        }

        /// <summary>
        /// Mana property
        /// </summary>
        public int CurrentMana
        {
            get { return _currentMana; }
            set { _currentMana = Mathf.Clamp(value, 0, maxMana); }
        }

        /// <summary>
        /// Converts the ratio of mana to float
        /// </summary>
        /// <returns>A float indicating the ratio of current to max</returns>
        public float GetFloatMana()
        {
            return CurrentMana / (float)maxMana;
        }

        
        /// <summary>
        /// Resets this character's stats. Currently setting health to half test healing.
        /// </summary>
        public void Reset()
        {
            _currentHealth = maxHealth /2;
            _currentMana = maxMana;
        }

        /// <summary>
        /// Regenerate mana
        /// </summary>
        public void RegenMana()
        {
            CurrentMana += _manaRegen;
        }
    }
}

