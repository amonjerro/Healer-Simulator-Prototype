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
        public int currentHealth;

        public int currentStubborness;
        public int currentConfidence;


        public float stubbornessBurnRate;
        public float confidenceMapFactor;
    }
}

