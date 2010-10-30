
namespace RSSFeedLibrary
{
    public abstract class TaggedItemBase<T> : ITaggedItem<T>
    {
        public string Tag
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }
    }
}
