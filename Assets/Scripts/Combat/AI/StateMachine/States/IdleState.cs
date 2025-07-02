using Prototype;
using UnityEngine;

namespace Prototype.StateMachine
{
    /// <summary>
    /// The base idle state. The character will wait for a bit then test to see if it can transition to other states
    /// </summary>
    public class IdleState : AICharacterState
    {
        float timeToIdle = 2.0f;
        float timer;
        GreaterThanCondition<float> timeCondition;
        EqualsCondition<bool> enemiesPresent;
        EqualsCondition<bool> enemiesNotPresent;
        AndCondition enemiesAndTime;
        AndCondition timeNoEnemies;
        public IdleState(StateMachine<CharacterStates> sm, AIController c) : base(sm, c)
        {
            stateValue = CharacterStates.Idle;
            timeCondition = new GreaterThanCondition<float>(timeToIdle);
            enemiesPresent = new EqualsCondition<bool>(true);
            enemiesNotPresent = new EqualsCondition<bool>(false);
            enemiesAndTime = new AndCondition(timeCondition, enemiesPresent);
            timeNoEnemies = new AndCondition(timeCondition, enemiesNotPresent);

            Transition<CharacterStates> toWandering = new Transition<CharacterStates>();
            Transition<CharacterStates> toAcquireTarget = new Transition<CharacterStates>();
            toWandering.SetCondition(timeNoEnemies);
            toAcquireTarget.SetCondition(enemiesAndTime);

            transitions.Add(CharacterStates.Wandering, toWandering);
            transitions.Add(CharacterStates.FindTarget, toAcquireTarget);
        }

        protected override void OnEnter()
        {
            timer = 0f;
        }

        protected override void OnExit()
        {
            Flush();
        }

        protected override void OnUpdate()
        {
            // Do nothing for a sec
            timer += TimeUtil.GetDelta();

            timeCondition.SetValue(timer);
            bool enemies = AreThereEnemiesPresent();
            enemiesPresent.SetValue(enemies);
            enemiesNotPresent.SetValue(enemies);
        }

        private bool AreThereEnemiesPresent()
        {
            return controller.AreEnemiesPresent(false);
        }
    }
}