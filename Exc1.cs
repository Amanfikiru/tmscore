public static class Exc1
{
    public static void Run()
    {
        double doubleValue = 0.1;
        double doubleResult = doubleValue + doubleValue + doubleValue;

        Console.WriteLine($"Double Result: {doubleResult}");

        decimal decimalValue = 0.1m;
        decimal decimalResult = decimalValue + decimalValue + decimalValue;

        Console.WriteLine($"Decimal Result: {decimalResult}");
    }
}