namespace BaseTool.Movement
{
    public interface IJumpable
    {
        public bool CanJump { get; }

        public void Jump();
    }
}