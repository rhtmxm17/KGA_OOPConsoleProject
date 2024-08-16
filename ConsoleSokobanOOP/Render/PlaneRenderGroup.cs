
namespace ConsoleSokobanOOP
{
    public class PlaneRenderGroup : IRenderGroup
    {
        Point IRenderGroup.Anchor { get; set; }

        IEnumerable<IConsoleRender> IRenderGroup.Items => Items;

        public List<IConsoleRender> Items { get; private set; }

        public PlaneRenderGroup()
        {
        }

        public PlaneRenderGroup(int capacity)
        {
            Items = new(capacity);
        }

    }
}
