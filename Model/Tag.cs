using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count {  get; set; }
    }
}
