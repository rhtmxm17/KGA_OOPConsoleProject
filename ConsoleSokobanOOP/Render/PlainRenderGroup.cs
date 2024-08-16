
namespace ConsoleSokobanOOP
{
    public class PlainRenderGroup : IRenderGroup
    {
        #region IRenderGroup

        public Point Anchor { get; set; }
        IEnumerable<IConsoleRender> IRenderGroup.Items => Items;

        #endregion IRenderGroup

        public List<IConsoleRender> Items { get; private set; }

        public PlainRenderGroup()
        {
            Items = new();
        }

        public PlainRenderGroup(int capacity)
        {
            Items = new(capacity);
        }

    }
}
