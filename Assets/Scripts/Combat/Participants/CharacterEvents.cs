namespace Prototype
{
    public enum CharacterEventTypes
    {
        // Input Enums
        Movement,
        SetSkillTarget,
        SkillReady,
        SkillUse,
        PlayAudio,

        // Status Enums
        AbilityAvailabilityChange,
        DamageTaken,
        Death
    }

    /// <summary>
    /// Absrtact character event message wrapper. 
    /// This exists to allow classes to receive any type of character event
    /// </summary>
    public abstract class CharacterEvent
    {
        public CharacterEventTypes eventType { get; private set; }
        public abstract object EventValue { get; }

        public CharacterEvent(CharacterEventTypes eventType)
        {
            this.eventType = eventType;
        }
    }

    /// <summary>
    /// Generic type character event message wrapper.
    /// This exists to allow classes to emit any type of character event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CharacterEvent<T> : CharacterEvent
    {
        public T Value {  get; set; }

        public override object EventValue { get { return Value; } }

        public CharacterEvent(CharacterEventTypes t, T val) : base(t)
        {
            Value = val;
        }
    }

}