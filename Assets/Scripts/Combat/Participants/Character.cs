using Prototype.UI;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
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

        [SerializeField]
        List<AbilityDataObject> abilities;

        MemberStats statsUI;
        CharacterEventManager eventManager;
        Ability readiedAbility;
        

        void Awake()
        {
            data = new CharacterData();
            data.maxHealth = _characterDataAsset.MaxHealth;
            data.maxMana = _characterDataAsset.MaxMana;
            data.Reset();
            activeStatusEffects = new List<StatusEffect>();
            CharacterMovement movementRef = GetComponentInChildren<CharacterMovement>();
            eventManager = GetComponent<CharacterEventManager>();
            eventManager.onCharacterEvent += ProcessEvents;
            movementRef.SetMovementVelocity(_characterDataAsset.MoveSpeed);
        }

        private void Start()
        {
            eventManager.BroadcastStatusChange(MakeAbilityAvailabilityChangedEvent());
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

        private void OnDestroy()
        {
            eventManager.onCharacterEvent -= ProcessEvents;
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
        /// <param name="effect">The status effect to apply</param>
        public void AddStatusEffect(StatusEffect effect)
        {
            activeStatusEffects.Add(effect);
        }

        public void ResolveCombatEffect()
        {
            UpdateUI();
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

        private void ProcessEvents(CharacterEvent ev)
        {
            switch (ev.eventType)
            {
                case CharacterEventTypes.SkillReady:
                    ReadyAbility((int)ev.EventValue);
                    return;
                case CharacterEventTypes.SkillUse:
                    UseAbility((bool)ev.EventValue);
                    return;
                case CharacterEventTypes.SetSkillTarget:
                    ReadyTarget((int) ev.EventValue);
                    return;
                default:
                    return;
            }
        }

        /*
         Ability Management
        */

        private void ReadyAbility(int value)
        {
            readiedAbility = AbilityFactory.MakeAbility(abilities[value]);
        }
        
        private void ReadyTarget(int value)
        {
            SingleTargetAbility sta = (SingleTargetAbility) readiedAbility;
            sta.SetTarget(ServiceLocator.Instance.GetService<AIDirectorService>().GetFriendlyCharacterByIndex(value));
        }

        private void UseAbility(bool trigger)
        {
            if (trigger)
            {
                // Process costs
                int cost = readiedAbility.GetCost();
                if (cost > 0) {
                    eventManager.BroadcastStatusChange(MakeAbilityAvailabilityChangedEvent());
                }
                data.CurrentMana -= readiedAbility.GetCost();

                eventManager.BroadcastCharacterEvent(new CharacterEvent<AudioClip>(CharacterEventTypes.PlayAudio, readiedAbility.GetSoundClip()));
                readiedAbility.Execute();
                UpdateUI();
                return;
            }

            if (readiedAbility == null) return;

            readiedAbility.Cancel();
            readiedAbility = null;
        }

        private CharacterEvent MakeAbilityAvailabilityChangedEvent()
        {
            bool[] abilityAvailability = new bool[CombatConstants.MaxAbilities];
            for (int i = 0; i < CombatConstants.MaxAbilities; i++) { 
                if (i >= abilities.Count)
                {
                    abilityAvailability[i] = false;
                } else if (abilities[i].abilityInformation.cost > data.CurrentMana)
                {
                    abilityAvailability[i] = false;
                } else {
                    abilityAvailability[i] = true;
                }
            }

            return new CharacterEvent<bool[]>(CharacterEventTypes.AbilityAvailabilityChange, abilityAvailability);
        }
    }
}

