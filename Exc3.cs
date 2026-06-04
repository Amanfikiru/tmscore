public record EnrollmentRecord(
    string StudentId,
    string CourseCode,
    DateTime EnrolledAt
);

public class Course
{
    public required string Code { get; init; }

    public required string Title
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Title cannot be empty.");
    }

    public int Capacity
    {
        get;
        set => field = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public int EnrolledCount { get; set; }
}