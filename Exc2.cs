public static class Exc2
{
    public static void Run()
    {
        string? region = null;

        Console.WriteLine(
            $"Region (conditional): {region?.ToUpper() ?? "unassigned"}");

        region ??= "Addis Ababa";

        Console.WriteLine($"Region (assigned): {region}");
    }
}