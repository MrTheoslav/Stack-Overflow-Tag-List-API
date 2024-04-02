using DAL;
using Model;
using Stack_Overflow_Tag_List_API.Interfaces;

namespace Stack_Overflow_Tag_List_API.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _dataContext;

        public TagRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool AddTag(Tag tag)
        {
            _dataContext.Add(tag);
            return Save();
        }

        public ICollection<Tag> GetAllTagsSortedByName(bool isAscending)
        {
            ICollection<Tag> result;
            if (isAscending)
                result = _dataContext.Tags.OrderBy(t => t.Name).ToList();
            else
                result = _dataContext.Tags.OrderByDescending(t => t.Name).ToList();
            return result;
        }
        public ICollection<Tag> GetAllTagsSortedByCount(bool isAscending)
        {
            ICollection<Tag> result;
            if (isAscending)
                result = _dataContext.Tags.OrderBy(t => t.Count).ToList();
            else
                result = _dataContext.Tags.OrderByDescending(t => t.Count).ToList();
            return result;
        }

        public bool UpdateTag(Tag tag)
        {
            var test = _dataContext.Update(tag);
            return Save();
        } 

        public Tag GetTag(string name)
        {
            Tag result = _dataContext.Tags.Where(t => t.Name == name).FirstOrDefault();
            return result;
        }

        public bool AnyTagExists()
        {
            return _dataContext.Tags.Any();
        }
    }
}
