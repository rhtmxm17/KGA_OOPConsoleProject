namespace ConsoleSokobanOOP
{
    public interface IRenderGroup
    {
        public Point Anchor { get; set; }
        public abstract IEnumerable<IConsoleRender> Items { get; }

        public virtual void GroupRender()
        {
            foreach (var item in Items)
            {
                item.Render(Anchor);
            }
        }
    }

    public static partial class Extention
    {
        public static void GroupRender(this IRenderGroup obj) => obj.GroupRender();
    }
}
