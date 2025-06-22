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
        private int _currentHealth;
        private int _currentStubborness;
        private int _currentConfidence;

        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }

            set
            {
                if (value < 0)
                {
                    _currentHealth = 0;
                    return;
                }
                if (value > maxHealth)
                {
                    _currentHealth = maxHealth;
                    return;
                }
                _currentHealth = value;
            }
        }

        public int CurrentConfidence
        {
            get
            {
                return _currentConfidence;
            }

            set
            {
                if (value < 0)
                {
                    _currentConfidence = 0;
                    return;
                }
                if (value > CombatConstants.MaxConfidence)
                {
                   _currentConfidence = CombatConstants.MaxConfidence;
                    return;
                }
                _currentConfidence = value;
            }
        }


        public int CurrentStubborness
        {
            get { return _currentStubborness; }
            set
            {
                if (value < 0)
                {
                    _currentStubborness = 0;
                    return;
                }
                if (value > CombatConstants.MaxStubborness)
                {
                    _currentStubborness = CombatConstants.MaxStubborness;
                    return;
                }
                _currentStubborness = value;

            }
        }


        public float stubbornessBurnRate;
        public float confidenceMapFactor;

    }
}

