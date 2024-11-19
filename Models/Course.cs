using Class2.Model;

namespace Class2.Model
{
    public class Course
        {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        }
}