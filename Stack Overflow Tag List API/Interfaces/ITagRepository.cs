using Model;

namespace Stack_Overflow_Tag_List_API.Interfaces
{
    public interface ITagRepository
    {
        bool AddTag(Tag tag);
        bool AnyTagExists();
        ICollection<Tag> GetAllTagsSortedByCount(bool isAscending);
        ICollection<Tag> GetAllTagsSortedByName(bool isAscending);
        Tag GetTag(string name);
        bool UpdateTag(Tag tag);
    }
}
