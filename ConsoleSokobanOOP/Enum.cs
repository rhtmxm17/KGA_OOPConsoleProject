namespace ConsoleSokobanOOP
{
    public enum SceneType { Title, Setting, Select, Stage };
    public enum TileState { Blocked, Moveable, Empty };
    public enum RenderMode { Default, Safe };

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        NONE,
    }
}