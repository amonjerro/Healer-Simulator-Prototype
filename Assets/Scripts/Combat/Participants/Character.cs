using Prototype.UI;
using System.Collections;
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
        [Tooltip("The data asset for this character")]
        CharacterDataAsset _characterDataAsset;

        [SerializeField]
        [Tooltip("The data assets for this characters abilities")]
        List<AbilityDataObject> abilities;

        CharacterData data;
        List<StatusEffect> activeStatusEffects;
        MemberStats statsUI;
        PowerContainer powerContainer;
        CharacterEventManager eventManager;
        Ability readiedAbility;

        bool isPlayer;
        
        /*
         Unity Lifecycle hooks
         */
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
            CharacterEventManager.onUIRequest += ProcessUIRequest;
            movementRef.SetMovementVelocity(_characterDataAsset.MoveSpeed);

            TimeUtil.onTick += HandleTick;
        }

        private void Start()
        {
            // Update controllers with availability
            EmitAbilityAvailabilityChangedEvent();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnDestroy()
        {
            // Unsuscribe from events
            eventManager.onCharacterEvent -= ProcessEvents;
            CharacterEventManager.onUIRequest -= ProcessUIRequest;
            TimeUtil.onTick -= HandleTick;
        }

        /*
            Data management 
        */

        /// <summary>
        /// Gets this characters stats
        /// </summary>
        /// <returns>A reference to the characters stats</returns>
        public CharacterData GetCharacterData()
        {
            return data;
        }

        public bool IsAlive()
        {
            return data.CurrentHealth > 0;
        }

        /// <summary>
        /// Add a status effect to this character
        /// </summary>
        /// <param name="effect">The status effect to apply</param>
        public void AddStatusEffect(StatusEffect effect)
        {
            activeStatusEffects.Add(effect);
        }

        /// <summary>
        /// Handles communicating to others that a combat effect has resolved
        /// </summary>
        /// <param name="value">The value of the effect</param>
        public void ResolveCombatEffect(int value)
        {
            if (IsAlive())
            {
                CharacterEvent<int> combatEffectEvent = new CharacterEvent<int>(CharacterEventTypes.DamageTaken, value);
                eventManager.BroadcastCharacterEvent(combatEffectEvent);
            } else
            {
                CharacterEvent<int> deathEvent = new CharacterEvent<int>(CharacterEventTypes.Death, value);
                eventManager.BroadcastStatusChange(deathEvent);
                gameObject.layer = LayerMask.NameToLayer("Dead");
            }
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

        /*
            UI Configuration 
        */

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

        public void SetPowersContainer(PowerContainer pc)
        {
            powerContainer = pc;
        }

        /*
            Private methods 
        */

        /// <summary>
        /// Updates the stat values of the UI to provide players with feedback
        /// </summary>
        private void UpdateUI()
        {
            if (statsUI == null) {
                return;
            }
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

        /// <summary>
        /// Process events published through the event manager
        /// </summary>
        /// <param name="ev">The event that was emitted</param>
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
                    ReadyTarget(ev.EventValue as Character);
                    return;
                default:
                    return;
            }
        }

        private void ProcessUIRequest(CharacterEvent ev)
        {
            switch (ev.eventType)
            {
                case CharacterEventTypes.UITargetRequest:
                    if (statsUI != null)
                    {
                        statsUI.ShowInputButton();
                    }
                    if (powerContainer != null)
                    {
                        powerContainer.HideButtonImages();
                    }
                    break;
                case CharacterEventTypes.UIAbilityRequest:
                    if (statsUI != null)
                    {
                        statsUI.HideInputButton();
                    }
                    if (powerContainer != null)
                    {
                        powerContainer.ShowButtonImages();
                    }
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Handle the behavior associated with the TimeUtil ticking
        /// </summary>
        private void HandleTick()
        {
            data.RegenMana();
            UpdateUI();
            EmitAbilityAvailabilityChangedEvent();
            StartCoroutine(StatusEffectUpdate());
        }

        /// <summary>
        /// Handling status effects can take a second and impact performance. There's no need for all
        /// of them to happen in the same single frame
        /// </summary>
        /// <returns></returns>
        IEnumerator StatusEffectUpdate()
        {
            // List of indeces of expired status effects to clean up
            List<int> cleanUp = new List<int>();

            // Iterate through current status effects to either apply them or mark them as expired;
            for (int i = 0; i < activeStatusEffects.Count; i++)
            {
                activeStatusEffects[i].Update(data);
                if (!activeStatusEffects[i].IsActive)
                {
                    cleanUp.Add(i);
                }
                yield return null;
            }

            // Perform cleanup
            for (int i = cleanUp.Count - 1; i >= 0; i--)
            {
                activeStatusEffects.RemoveAt(cleanUp[i]);
            }
        }


        /*
         Ability Management
        */

        /// <summary>
        /// Stage an ability to be executed
        /// </summary>
        /// <param name="value">The index that indicates which ability to ready</param>
        private void ReadyAbility(int value)
        {
            readiedAbility = AbilityFactory.MakeAbility(abilities[value]);
        }
        
        /// <summary>
        /// Set the target for this ability
        /// </summary>
        /// <param name="value">The character to target</param>
        protected virtual void ReadyTarget(Character value)
        {
            SingleTargetAbility sta = (SingleTargetAbility) readiedAbility;
            sta.SetTarget(value);
        }


        /// <summary>
        /// Do something with the ready ability
        /// </summary>
        /// <param name="trigger">Execute or cancel. True is execute.</param>
        private void UseAbility(bool trigger)
        {
            if (trigger)
            {
                // Process costs
                int cost = readiedAbility.GetCost();
                data.CurrentMana -= readiedAbility.GetCost();
                if (cost > 0)
                {
                    EmitAbilityAvailabilityChangedEvent();   
                }

                // Ability Execution
                eventManager.BroadcastCharacterEvent(new CharacterEvent<AudioClip>(CharacterEventTypes.PlayAudio, readiedAbility.GetSoundClip()));
                readiedAbility.Execute();
                readiedAbility.Resolve();

                UpdateUI();
                return;
            }

            if (readiedAbility == null) return;

            // Else cancel all of this
            readiedAbility.Cancel();
            readiedAbility = null;
        }

        /// <summary>
        /// Emit a message indicating how abilities may have changed. 
        /// The emited event holds an array of booleans that say which abilities are available.
        /// </summary>
        private void EmitAbilityAvailabilityChangedEvent()
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
            CharacterEvent<bool[]> ev = new CharacterEvent<bool[]>(CharacterEventTypes.AbilityAvailabilityChange, abilityAvailability);
            eventManager.BroadcastStatusChange(ev);
            
        }
    }
}

