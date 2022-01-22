namespace Game.Enemy
{
    public readonly struct ToggleAction
    {
        public readonly bool IsActive;
        public readonly Action Target;

        public ToggleAction(bool isActive, Action target)
        {
            IsActive = isActive;
            Target = target;
        }
    }
}
