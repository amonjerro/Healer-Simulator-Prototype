using System.Collections.Generic;
using UnityEngine;
namespace Prototype
{
    public class Character : MonoBehaviour
    {
        CharacterData data;
        List<StatusEffect> activeStatusEffects;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            data = new CharacterData();
            activeStatusEffects = new List<StatusEffect>();
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
    }
}

