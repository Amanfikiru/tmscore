using System.Diagnostics; 

// THE WRONG WAY: Blocking with Thread.Sleep 
var sw = Stopwatch.StartNew(); 
for (int i = 0; i < 5; i++) 
{ 
    Thread.Sleep(300);  // Thread is HELD for 300ms  cannot serve anyone else 
} 
Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms"); 
 
// ASYNC BUT STILL SEQUENTIAL: Thread released, but calls are one-at-a-time 
sw.Restart(); 
for (int i = 0; i < 5; i++) 
{ 
    await Task.Delay(300);  // Thread released while waiting  but still sequential 
} 
Console.WriteLine($"Async sequential:   {sw.ElapsedMilliseconds}ms"); 
 
// THE RIGHT WAY: Async parallel  all 5 start simultaneously 
sw.Restart(); 
var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300)); 
await Task.WhenAll(tasks); 
Console.WriteLine($"Async parallel:     {sw.ElapsedMilliseconds}ms");

async Task<Student> FetchStudentAsync(string id) 
{ 
    Console.WriteLine($"  Fetching {id}..."); 
    await Task.Delay(300);  // Simulate database latency 
    return new Student 
    { 
        Id = id, 
        Name = $"Student-{id}", 
        Age = 20, 
        GPA = id switch 
        { 
            "S1" => 3.8m, 
            "S2" => 2.4m, 
            "S3" => 3.5m, 
            "S4" => 1.9m, 
            "S5" => 3.2m, 
            _ => 2.5m 
        } 
    }; 
} 
async Task<Course> FetchCourseAsync(string code) 
{ 
    Console.WriteLine($"  Fetching course {code}..."); 
    await Task.Delay(200);  // Simulate database latency 
    return new Course 
    { 
        Code = code, 
        Title = $"Course-{code}", 
        Capacity = code switch 
        { 
            "CRS-101" => 2, 
            "CRS-201" => 30, 
            "CRS-301" => 15, 
            _ => 25 
        } 
    }; 
} 
sw.Restart(); 
 
// Start all fetches simultaneously  students AND courses 
string[] studentIds = ["S1", "S2", "S3", "S4", "S5"]; 
string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"]; 
 
var studentTasks = studentIds.Select(id => FetchStudentAsync(id)); 
var courseTasks = courseCodes.Select(code => FetchCourseAsync(code)); 
 
// Both arrays load concurrently 
Student[] students = await Task.WhenAll(studentTasks); 
Course[] courses = await Task.WhenAll(courseTasks); 
 
Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms"); 
foreach (var s in students) 
{ 
Console.WriteLine($"  {s.Name}  GPA: {s.GPA}"); 
}

async Task SendConfirmationAsync(Student student) 
{ 
    try 
    { 
        await Task.Delay(100);  // Simulate sending email 
        Console.WriteLine($"  Email sent to {student.Name}"); 
    } 
    catch (Exception ex) 
    { 
        // Log the failure  do NOT re-throw. 
        // This is intentional fire-and-forget. 
        Console.WriteLine($"  Email failed for {student.Name}: {ex.Message}"); 
    } }

var enrollService = new EnrollmentService();

// COURSE USED FOR ENROLLMENT LOOP (Part B requirement)
var enrollCourse = new Course
{
    Code = "CRS-101",
    Title = "C# Mastery",
    Capacity = 2
};

var enrollments = new List<EnrollmentRecord>();
var failures = new List<string>();

// ENROLLMENT LOOP (THIS IS REQUIRED BY EXERCISE 6 PART B)
foreach (var student in students)
{
    try
    {
        var record = enrollService.ProcessRegistration(student, enrollCourse);

        enrollCourse.EnrolledCount++;

        enrollments.Add(record);

        Console.WriteLine($"  Enrolled: {student.Name}");

        // Optional (Exercise 6B behavior)
        _ = SendConfirmationAsync(student);
    }
    catch (CapacityReachedException ex)
    {
        failures.Add($"{student.Name}: {ex.Message}");

        Console.WriteLine($"  Rejected: {student.Name} - {ex.Message}");
    }
}
    // Stop the timer 
sw.Stop(); 
// Calculate class average GPA from loaded students 
decimal classAverage = students.Length > 0 
? students.Average(s => s.GPA) 
: 0m; 
// Print the final report 
Console.WriteLine("\n========== ENROLLMENT SUMMARY =========="); 
Console.WriteLine($"Total students loaded:  {students.Length}"); 
Console.WriteLine($"Successful enrollments:     {enrollments.Count}"); 
Console.WriteLine($"Failed enrollments:         {failures.Count}"); 
Console.WriteLine($"Class average GPA:          {classAverage:F2}"); 
Console.WriteLine($"Total elapsed time:         {sw.ElapsedMilliseconds}ms"); 
 
if (failures.Count > 0) 
{ 
    Console.WriteLine("\n--- Failure Details ---"); 
    foreach (var failure in failures) 
    { 
        Console.WriteLine($"  {failure}"); 
    } 
} 
 
Console.WriteLine("========================================"); 