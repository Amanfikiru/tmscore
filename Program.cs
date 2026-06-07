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

    try 
    { 
        var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Test", Capacity = 1, EnrolledCount = 1 }; 
        enrollService.ProcessRegistration( 
            new Student { Id = "S99", Name = "Test", Age = 20, GPA = 3.0m }, 
            overflowCourse 
        ); 
    } 
    catch (CapacityReachedException ex) 
    { 
        Console.WriteLine($"\nDomain exception caught:"); 
        Console.WriteLine($"  Course: {ex.CourseCode}"); 
        Console.WriteLine($"  Message: {ex.Message}"); 
    } 