Exc1.Run();

Console.WriteLine();

Exc2.Run();

Console.WriteLine();

var enrollment = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    DateTime.UtcNow);

Console.WriteLine(enrollment);

var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
Console.WriteLine(course.Title);

 
var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}"); 
Console.WriteLine();


void PrintGradeReport(IEnumerable<IGradable> assessments) 
{ 
    Console.WriteLine("--- Grade Report ---"); 
    foreach (var item in assessments) 
    { 
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%"); 
    } 
} 
IGradable[] cohortAssessments = [ 
    new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 }, 
    new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m } 
]; 
 
PrintGradeReport(cohortAssessments); 


//Enrollment Services 
var service = new EnrollmentService();
// Test 1: Valid registration
var validStudent = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

var validCourse = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

var result = service.ProcessRegistration(validStudent, validCourse);

Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

// Test 2: Null student
try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}");
}

// Test 3: Full course
var fullCourse = new Course
{
    Code = "CS-402",
    Title = "Full Course",
    Capacity = 1
};

fullCourse.EnrolledCount = 1;

try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}");
}

//analytics dashboard
Console.WriteLine("");
Console.WriteLine("--- Student Lead Board ---");
// C# 12+ Collection Expressions  the modern way to initialize lists 
List<Student> students = [ 
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m }, 
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m }, 
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m }, 
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m }, 
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m }, 
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m }, 
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m }, 
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m } 
]; 

var leaderboard = students 
    // TODO 1: Extract students where GPA is >= 3.5m 
    // TODO 2: Sort the remaining students by GPA descending 
    // TODO 3: Project the result so we only keep the 'Name' string 
    // TODO 4: Materialize the lazy query into a concrete List 
    .Where(s => s.GPA >= 3.5m)
    .OrderByDescending(s => s.GPA)
    .Select(s => s.Name)
    .ToList();

Console.WriteLine($"Found {leaderboard.Count} Honors Students:"); 
foreach (var name in leaderboard) 
{ 
    Console.WriteLine($"- {name}"); 
}

// TODO 5: Use LINQ to calculate the average GPA across all students.        
decimal averageGpa = students.Average(s => s.GPA);
// Stuck? Pattern: students.Average(s => s.SomeProperty) 
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}"); 

// TODO 6: Use .GroupBy with a switch expression to classify each student. 
var standingGroups = students .GroupBy(s => s.GPA switch
    {
        >= 3.5m => "Honors",
        >= 2.5m => "Good Standing",
        >= 2.0m => "Probation",
        _ => "Academic Warning"
    });

Console.WriteLine("\n--- Academic Standing Report ---");

foreach (var group in standingGroups)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()}):");

    foreach (var stud in group)
    {
        Console.WriteLine($"  {stud.Name} - GPA: {stud.GPA}");
    }
}

// TODO 7: Use the spread operator (..) to merge two arrays and append a value. 
string[] backendCourses = ["C#", "ASP.NET Core"]; 
string[] frontendCourses = ["TypeScript", "Angular"]; 
string[] allCourses = [..backendCourses, ..frontendCourses, "Capstone"];
Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}"); 