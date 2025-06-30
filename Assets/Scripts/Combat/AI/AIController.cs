using UnityEngine;
using Prototype.StateMachine;
using System.Collections.Generic;

namespace Prototype
{
    /// <summary>
    /// AI Character controller.
    /// This class acts as a counterpart to the player controller and sends the same signals
    /// to the character class via events
    /// </summary>
    public class AIController : MonoBehaviour
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
        CharacterEventManager eventManager;
        AIDirectorService directorRef;
        AbsAIStrategy strategy;
        Character targetRef;

        private void Start()
        {
            eventManager = GetComponentInParent<CharacterEventManager>();
            Character character = GetComponentInParent<Character>();
            directorRef = ServiceLocator.Instance.GetService<AIDirectorService>();
            directorRef.RegisterActor(CharacterAttitude, character);
            stateMachine = new StateMachine<CharacterStates>();
            strategy = AIStrategyFactory.MakeStrategy(aiStrategy, this);
            oppositeMatch = new Dictionary<ActorAttitude, ActorAttitude>() { { ActorAttitude.Friendly, ActorAttitude.Hostile }, { ActorAttitude.Hostile, ActorAttitude.Friendly } };
            SetupStateMachine();
        }

        /// <summary>
        /// Sets up the state machine for an AI controller. Part of initialization.
        /// Internals delegated to AI strategy
        /// </summary>
        protected void SetupStateMachine()
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

        public void PublishMessage(CharacterEvent ev)
        {
            eventManager.BroadcastCharacterEvent(ev);
        }

        public ActorAttitude GetAttitude()
        {
            return CharacterAttitude;
        }

        public void SetTarget(Character c)
        {
            targetRef = c;
        }
        
        public Character GetTarget()
        {
            return targetRef;
        }

        public Character FindTarget(bool same)
        {
            if (same)
            {
                return strategy.FindNextTarget(CharacterAttitude);
            }
            return strategy.FindNextTarget(oppositeMatch[CharacterAttitude]);
        }
    }
}