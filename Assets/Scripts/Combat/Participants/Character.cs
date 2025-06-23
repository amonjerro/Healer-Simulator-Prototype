using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
namespace Prototype
{
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
            List<int> cleanUp = new List<int>();
            for(int i = 0; i < activeStatusEffects.Count; i++)
            {
                activeStatusEffects[i].Update(data);
                if (!activeStatusEffects[i].IsActive)
                {
                    cleanUp.Add(i);
                }
            }

            for (int i = cleanUp.Count -1; i >= 0; i--)
            {
                activeStatusEffects.RemoveAt(cleanUp[i]);
            }
        }

        public CharacterData GetCharacterData()
        {
            return data;
        }

        public void AddStatusEffect(StatusEffectData dataConfig)
        {
            StatusEffect effect = new StatusEffect(dataConfig);
            activeStatusEffects.Add(effect);
        }

        public void SetCharacterAsset(CharacterDataAsset asset)
        {
            _characterDataAsset = asset;
        }
    }
}

