
namespace RSSFeedLibrary
{
    public interface ITaggedItem<T>
    {
        string Tag { get; set; }
        T Value { get; set; }
    }
}
