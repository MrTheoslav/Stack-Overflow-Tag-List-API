using DAL;
using Model;
using Stack_Overflow_Tag_List_API.Interfaces;

namespace Stack_Overflow_Tag_List_API.Services
{
    public class DatabaseTagService : IDatabaseTagService
    {
        private readonly DataContext _dataContext;

        public DatabaseTagService(DataContext dataContext)
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

        public bool AddTags(IEnumerable<Tag> tags)
        {
            _dataContext.Add(tags);
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
                result = _dataContext.Tags.OrderBy(t => t.Name).ToList();
            else
                result = _dataContext.Tags.OrderByDescending(t => t.Name).ToList();
            return result;
        }

        public Tag GetTag(string name)
        {
            Tag result = _dataContext.Tags.Where(t=>t.Name == name).FirstOrDefault();
            return result;
        }
        
        public bool AnyTagExists()
        {
            return _dataContext.Tags.Any();
        }
    }
}
