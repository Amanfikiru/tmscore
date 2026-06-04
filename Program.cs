string? region = null;

Console.WriteLine($"Region (conditional): {region?.ToUpper()}");

string upperRegion = region ?? "unassinged";
Console.WriteLine($"Region (conditional): {upperRegion}");

region ??= "Addis ababa";
Console.WriteLine($"Region (conditional): {region}");
