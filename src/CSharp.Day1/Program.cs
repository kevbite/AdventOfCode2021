var lines = File.ReadAllLines("input.txt")
    .Select(int.Parse)
    .ToList();

void Part1()
{
    var count = lines
        .Zip(lines.Skip(1))
        .Count(x => x.Second > x.First);

    Console.WriteLine($"There are {count} measurements that increased");
}

void Part2()
{
    var windows = lines
        .Zip(lines.Skip(1), lines.Skip(2))
        .Select(x => x.First + x.Second + x.Third)
        .ToList();
    
    var count = windows.Zip(windows.Skip(1))
        .Count(x => x.Second > x.First);

    Console.WriteLine($"There are {count} measurements that increased");
}

Part1();
Part2();
