using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public int Count {  get; set; }
        public string Contribution { get; set; }
    }
}
