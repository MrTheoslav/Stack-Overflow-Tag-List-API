using Stack_Overflow_Tag_List_API.Interfaces;
using System.Globalization;

namespace Stack_Overflow_Tag_List_API.Services
{
    public class Operations : IOperations
    {
        public string Participation(decimal baseCount, decimal wholeCount)
        {
            string result;

            result = ((baseCount / wholeCount) * 100m).ToString("G2", CultureInfo.InvariantCulture) + "%";

            return result;
        }
    }
}
