using Class2.Model;

namespace Class2.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}