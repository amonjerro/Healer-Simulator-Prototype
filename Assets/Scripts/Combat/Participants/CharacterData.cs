using UnityEngine;

namespace Prototype
{
    public enum Personality
    {
        Neutral,
        Nervous,
        Confident,
        Stubborn,
        Compliant
    }


    public class CharacterData
    {
        public int maxHealth;
        public int maxMana;
        private int _currentMana;
        private int _manaRegen = 2;
        private int _currentHealth;
        private int _currentStubborness;
        private int _currentConfidence;

        public int CurrentHealth
        {
            get { return _currentHealth; }

            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public float GetFloatHealth()
        {
            return CurrentHealth / (float)maxHealth;
        }

        public int CurrentConfidence
        {
            get { return _currentConfidence; }

            set { _currentConfidence = Mathf.Clamp(value, 0, CombatConstants.MaxConfidence); }
        }

        public int CurrentStubborness
        {
            get { return _currentStubborness; }
            set { _currentStubborness = Mathf.Clamp(value, 0, CombatConstants.MaxStubborness); }
        }

        public float GetFloatStubborn()
        {
            return CurrentStubborness / (float)CombatConstants.MaxStubborness;
        }


        public int CurrentMana
        {
            get { return _currentMana; }
            set { _currentMana = Mathf.Clamp(value, 0, maxMana); }
        }

        public float GetFloatMana()
        {
            return CurrentMana / (float)maxMana;
        }

        public float stubbornessBurnRate;
        public float confidenceMapFactor;


        public void Reset()
        {
            _currentHealth = maxHealth /2;
            _currentMana = maxMana;
        }

        public void RegenMana()
        {
            CurrentMana += _manaRegen;
        }
    }
}

