namespace Prototype
{
    public abstract class AIStrategy
    {
        protected Character characterInformation;

        public AIStrategy(Character characterInformation)
        {
            this.characterInformation = characterInformation;
        }
    }
}