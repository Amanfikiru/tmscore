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