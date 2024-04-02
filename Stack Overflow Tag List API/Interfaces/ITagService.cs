using Model;

namespace Stack_Overflow_Tag_List_API.Interfaces
{
    public interface ITagService
    {
        List<Tag> CountParticipation();
        Task<bool> GetTags();
        bool PutTagsToRepository();
    }
}
