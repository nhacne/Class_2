using System.Net;
using System.Text.Json;
using Class2.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;

public class ClassApiFunction
{
    private readonly ApplicationDbContext _context;

    public ClassApiFunction(ApplicationDbContext context)
    {
        _context = context;
    }

    // ADD CLASS
    [Function("AddClass")]
    public async Task<HttpResponseData> AddClass(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var requestBody = await JsonSerializer.DeserializeAsync<Class>(req.Body);

        _context.Classes.Add(requestBody);
        await _context.SaveChangesAsync();
        
        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(requestBody);
        return response;
    }

    // GET CLASSES
    [Function("GetClasses")]
    public async Task<HttpResponseData> GetClasses (
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        var classes = await _context.Classes.ToListAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(classes);
        return response;
    }

    // UPDATE CLASS
    [Function("UpdateClass")]
    public async Task<HttpResponseData> UpdateClass (
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "classes/{id}")] HttpRequestData req, int id)
    {
        var _class = await _context.Classes.FindAsync(id);
        var updateClass = await JsonSerializer.DeserializeAsync<Class> (req.Body);

        _class.ClassName = updateClass.ClassName;
        await _context.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_class);

        return response;
    }

    // DELETE CLASS
    [Function("DeleteClass")]
    public async Task<HttpResponseData> deleteClass (
        [HttpTrigger (AuthorizationLevel.Function, "delete", Route ="classes/{id}")] HttpRequestData req, int id)
    {
        var classes = await _context.Classes.FindAsync(id);
        _context.Classes.Remove(classes);
        await _context.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.NoContent);
        return response;
    }

    // ADD COURSE
    [Function("AddCourse")]
    public async Task<HttpResponseData> AddCourse(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var requestBody = await JsonSerializer.DeserializeAsync<Course>(req.Body);

        _context.Courses.Add(requestBody);
        await _context.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(requestBody);
        return response;
    }

    // GET COURSE
    [Function("GetCourse")]
    public async Task<HttpResponseData> GetCourse (
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        var course = await _context.Courses.ToListAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(course);
        return response;
    }

    // UPDATE COURSE
    [Function("UpdateCourse")]
    public async Task<HttpResponseData> UpdateCourse(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "courses/{id}")] HttpRequestData req, int id)
    {
        var _course = await _context.Courses.FindAsync(id);

        var updatedData = await JsonSerializer.DeserializeAsync<Course>(req.Body);

        _course.CourseName = updatedData.CourseName;

        await _context.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_course);
        return response;
    }

    // DELETE COURSE
    [Function("DeleteCourse")]
    public async Task<HttpResponseData> DeleteCourse(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "courses/{id}")] HttpRequestData req, int id)
    {
        var course = await _context.Courses.FindAsync(id);
       
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return req.CreateResponse(HttpStatusCode.NoContent);
    }

    // ADD STUDENT
    [Function("AddStudent")]
    public async Task<HttpResponseData> AddStudent(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
            var requestBody = await JsonSerializer.DeserializeAsync<Student>(req.Body);

            _context.Students.Add(requestBody);

            // SAVE CHANGE
            await _context.SaveChangesAsync();

            // RESPONSE
            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(requestBody);
            return response;
       
    }
   
   // ADD STUDENT TO CLASS
   [Function("AddStudent_ToClass")]
   public async Task<HttpResponseData> AddStudentToClass(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route ="studentClass/{studentId}/{classId}")] HttpRequestData req, int studentId, int classId)
    {   
        var student = await _context.Students.FindAsync(studentId);
        if (student == null)
        {
        var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
        await responseNotFound.WriteStringAsync("Student not found !.");
        return responseNotFound;
        }

        var _class = await _context.Classes.FindAsync(classId);
        if (_class == null)
        {
        var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
        await responseNotFound.WriteStringAsync("Class not found !.");
        return responseNotFound;
        }

        var studentClass = new StudentClass
        {
            StudentId = studentId,
            ClassId = classId
        };

        student.StudentClasses.Add(studentClass);
        
        // SAVE CHANGE
        await _context.SaveChangesAsync();

        // RESPONSE
        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteStringAsync("Add student to class is successfull !.");
        return response;
    }

    // GET STUDENT
    [Function("GetStudents")]
    public async Task<HttpResponseData> GetStudents(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
            var students = await _context.Students.ToListAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(students);
            return response;
    }

    // UPDATE STUDENT
    [Function("UpdateStudent")]
    public async Task<HttpResponseData> UpdateStudent(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "students/{id}")] HttpRequestData req, int id)
    {
       
            var existingStudent = await _context.Students.FindAsync(id);

            var updatedData = await JsonSerializer.DeserializeAsync<Student>(req.Body);

            existingStudent.Name = updatedData.Name;
            existingStudent.Email = updatedData.Email;
            existingStudent.ClassId = updatedData.ClassId;

            await _context.SaveChangesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(existingStudent);
            return response;
      
    }

    // DELETE STUDENT
    [Function("DeleteStudent")]
    public async Task<HttpResponseData> DeleteStudent(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "students/{id}")] HttpRequestData req, int id)
    {

            var student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return req.CreateResponse(HttpStatusCode.NoContent);
    }
}
