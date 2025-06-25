using Prototype.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        MemberStats statsUI;


        void Awake()
        {
            data = new CharacterData();
            data.maxHealth = _characterDataAsset.MaxHealth;
            data.maxMana = _characterDataAsset.MaxMana;
            data.Reset();
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

        /// <summary>
        /// Set the reference for this character UI and configure it
        /// </summary>
        /// <param name="statsElement"></param>
        public void SetStatsUI(MemberStats statsElement)
        {
            statsUI = statsElement;
            UpdateUI();
            SetPortraitUI();
        }

        /// <summary>
        /// Updates the stat values of the UI to provide players with feedback
        /// </summary>
        private void UpdateUI()
        {
            statsUI.SetHealthValue(data.GetFloatHealth());
            statsUI.SetManaValue(data.GetFloatMana());
            statsUI.SetStubbornessValue(data.GetFloatStubborn());
        }


        /// <summary>
        /// Sets this character's portrait in the UI
        /// </summary>
        private void SetPortraitUI()
        {
            CharacterView characterView = GetComponentInChildren<CharacterView>();
            statsUI.SetPortrait(characterView.GetSprite());
        }
    }
}

