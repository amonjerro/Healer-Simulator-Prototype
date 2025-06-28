namespace Prototype
{

    public enum CharacterEventTypes
    {
        // Input Enums
        Movement,
        SetSkillTarget,
        SkillReady,
        SkillUse,

        // Status Enums
        AbilityAvailabilityChange,
        DamageTaken,
        Death
    }

    public abstract class CharacterEvent
    {
        public CharacterEventTypes eventType { get; private set; }
        public abstract object EventValue { get; }

        public CharacterEvent(CharacterEventTypes eventType)
        {
            this.eventType = eventType;
        }
    }

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