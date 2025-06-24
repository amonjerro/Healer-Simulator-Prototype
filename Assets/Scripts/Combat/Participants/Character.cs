using System.Collections.Generic;
using UnityEngine;
namespace Prototype
{
    /// <summary>
    /// The central interface for this character.
    /// Should be the single point of access for any non-Chracter component.
    /// </summary>
    public class Character : MonoBehaviour
    {
        [SerializeField]
        CharacterDataAsset _characterDataAsset;

        CharacterData data;
        List<StatusEffect> activeStatusEffects;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            data = new CharacterData();
            data.maxHealth = _characterDataAsset.MaxHealth;
            activeStatusEffects = new List<StatusEffect>();
            CharacterMovement movementRef = GetComponentInChildren<CharacterMovement>();
            movementRef.SetMovementVelocity(_characterDataAsset.MoveSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            // List of indeces of expired status effects to clean up
            List<int> cleanUp = new List<int>();
            
            // Iterate through current status effects to either apply them or mark them as expired;
            for(int i = 0; i < activeStatusEffects.Count; i++)
            {
                activeStatusEffects[i].Update(data);
                if (!activeStatusEffects[i].IsActive)
                {
                    cleanUp.Add(i);
                }
            }

            // Perform cleanup
            for (int i = cleanUp.Count -1; i >= 0; i--)
            {
                activeStatusEffects.RemoveAt(cleanUp[i]);
            }
        }

        /// <summary>
        /// Gets this characters stats
        /// </summary>
        /// <returns>A reference to the characters stats</returns>
        public CharacterData GetCharacterData()
        {
            return data;
        }


        /// <summary>
        /// Add a status effect to this character
        /// </summary>
        /// <param name="dataConfig"></param>
        public void AddStatusEffect(StatusEffectData dataConfig)
        {
            StatusEffect effect = new StatusEffect(dataConfig);
            activeStatusEffects.Add(effect);
        }

        /// <summary>
        /// Set this character's data asset. Currently unused.
        /// </summary>
        /// <param name="asset">The scriptable object for this character</param>
        public void SetCharacterAsset(CharacterDataAsset asset)
        {
            _characterDataAsset = asset;
        }
    }
}

