Exc1.Run();

Console.WriteLine();

Exc2.Run();

Console.WriteLine();

var enrollment = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    DateTime.UtcNow);

Console.WriteLine(enrollment);

var course = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

Console.WriteLine(course.Title);