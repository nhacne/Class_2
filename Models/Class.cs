using Class2.Model;

namespace Class2.Model
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    }
}
