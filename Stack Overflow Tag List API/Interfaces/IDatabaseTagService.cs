using Model;

namespace Stack_Overflow_Tag_List_API.Interfaces
{
    public interface IDatabaseTagService
    {
        bool AddTag(Tag tag);
        bool AddTags(IEnumerable<Tag> tags);
        bool AnyTagExists();
        ICollection<Tag> GetAllTagsSortedByCount(bool isAscending);
        ICollection<Tag> GetAllTagsSortedByName(bool isAscending);
        Tag GetTag(string name);
    }
}
