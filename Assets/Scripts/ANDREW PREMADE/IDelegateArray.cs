namespace CustomEvents.Andrew
{
    public interface IDelegateArray
    {
        public delegate void EnumDelegate();
        public EnumDelegate[] EnumEvents { get; }
    }
}
