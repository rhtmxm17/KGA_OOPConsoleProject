
namespace ConsoleSokobanOOP
{
    public class PlaneRenderGroup : IRenderGroup
    {
        #region IRenderGroup

        public Point Anchor { get; set; }
        IEnumerable<IConsoleRender> IRenderGroup.Items => Items;

        #endregion IRenderGroup

        public List<IConsoleRender> Items { get; private set; }

        public PlaneRenderGroup()
        {
            Items = new();
        }

        public PlaneRenderGroup(int capacity)
        {
            Items = new(capacity);
        }

    }
}
