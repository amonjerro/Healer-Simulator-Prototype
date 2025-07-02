using UnityEngine;
using Prototype.StateMachine;
using System.Collections.Generic;
using System.Collections;

namespace Prototype
{
    /// <summary>
    /// AI Character controller.
    /// This class acts as a counterpart to the player controller and sends the same signals
    /// to the character class via events
    /// </summary>
    public class AIController : AbsCharacterController
    {

        [SerializeField]
        [Tooltip("Is this AI friendly or hostile?")]
        ActorAttitude CharacterAttitude;

        [SerializeField]
        [Tooltip("Debugging for the state machine. Changing this in runtime does nothing.")]
        CharacterStates activeState;

        [SerializeField]
        [Tooltip("The type of AI this is")]
        StrategyTypes aiStrategy;

        StateMachine<CharacterStates> stateMachine;
        Dictionary<ActorAttitude, ActorAttitude> oppositeMatch;
        AIDirectorService directorRef;
        AbsAIStrategy strategy;
        Character targetRef;

        private void Start()
        {
            eventManager = GetComponentInParent<CharacterEventManager>();
            eventManager.onStatusChange += ProcessEvents;
            Character character = GetComponentInParent<Character>();
            directorRef = ServiceLocator.Instance.GetService<AIDirectorService>();
            directorRef.RegisterActor(CharacterAttitude, character);
            stateMachine = new StateMachine<CharacterStates>();
            strategy = AIStrategyFactory.MakeStrategy(aiStrategy, this);
            oppositeMatch = new Dictionary<ActorAttitude, ActorAttitude>() { { ActorAttitude.Friendly, ActorAttitude.Hostile }, { ActorAttitude.Hostile, ActorAttitude.Friendly } };
            SetupStateMachine();
        }

        private void OnDestroy()
        {
            eventManager.onStatusChange -= ProcessEvents;
        }

        /// <summary>
        /// Sets up the state machine for an AI controller. Part of initialization.
        /// Internals delegated to AI strategy
        /// </summary>
        protected override void SetupStateMachine()
        {
            strategy.SetupAIStateMachine(stateMachine, this);
        }

        private void Update()
        {
            stateMachine.Update();
            activeState = stateMachine.GetCurrentState();
        }

        /// <summary>
        /// Determines a wander destination point. This might be moved to a more complex AI Strategy pattern down the line
        /// </summary>
        /// <returns>The x position to wander towards</returns>
        public float GetWanderDestination()
        {
            return directorRef.GetPlayerPosition() + Random.Range(-1.0f, 1.0f);
        }

        /// <summary>
        /// Tells the AI in which direction to move.
        /// </summary>
        /// <param name="direction">Negative values represent left.</param>
        public void MoveInDirection(float direction) {
            CharacterEvent ce = new CharacterEvent<float>(CharacterEventTypes.Movement, direction);
            eventManager.BroadcastCharacterEvent(ce);
        }

        /// <summary>
        /// Get this actor's attitude
        /// </summary>
        /// <returns></returns>
        public ActorAttitude GetAttitude()
        {
            return CharacterAttitude;
        }

        /// <summary>
        /// Stage a target reference which states can refer to
        /// </summary>
        /// <param name="c">A character to target with an ability</param>
        public void SetTarget(Character c)
        {
            targetRef = c;
        }

        /// <summary>
        /// Get the target reference
        /// </summary>
        /// <returns>The set target</returns>
        public Character GetTarget()
        {
            return targetRef;
        }

        /// <summary>
        /// Finds a target according to this controller's strategy
        /// </summary>
        /// <param name="same">Whether to search for other characters who share this character's attitude</param>
        /// <returns>A found target. Can return null.</returns>
        public Character FindTarget(bool same)
        {
            ActorAttitude searchTerm = same ? CharacterAttitude : oppositeMatch[CharacterAttitude];
            return strategy.FindNextTarget(searchTerm);
        }

        /// <summary>
        /// Asks the AI director to check if the roster of characters for the given attitude has elements in it
        /// </summary>
        /// <param name="same">Should the check be for characters who share this character's attitude</param>
        /// <returns>Whether opponents are present</returns>
        public bool AreEnemiesPresent(bool same)
        {
            ActorAttitude searchTerm = same ? CharacterAttitude : oppositeMatch[CharacterAttitude];
            return ServiceLocator.Instance.GetService<AIDirectorService>().GetCharacterRoster(searchTerm).Count > 0;
        }

        /// <summary>
        /// Delegates all handling of death to the strategy pattern
        /// </summary>
        public void HandleDeath()
        {
            strategy.HandleDeath();
        }

        /// <summary>
        /// Process any relevant character events
        /// </summary>
        /// <param name="ev">The character event to process</param>
        protected override void ProcessEvents(CharacterEvent ev)
        {
            switch (ev.eventType)
            {
                case CharacterEventTypes.Death:
                    stateMachine.SetAsTerminal();
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Get a reference to this controller's character.
        /// I really hate having to create dependencies like this and am thinking of ways of removing this function
        /// </summary>
        /// <returns>This controller's character</returns>
        public Character GetCharacter()
        {
            return GetComponentInParent<Character>();
        }

        /// <summary>
        /// Do cleanup of this object
        /// </summary>
        public void CleanUp()
        {
            Destroy(gameObject.transform.parent.gameObject, 2.0f);
        }
    }
}