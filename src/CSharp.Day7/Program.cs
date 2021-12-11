var positions = File.ReadAllLines("input.txt")
    .Single()
    .Split(',')
    .Select(int.Parse)
    .ToArray();
    
var min = positions.Min();
var max = positions.Max();

int CalculateFuel(Func<int, int> fuelSum)
{
    return Enumerable.Range(min, max - min + 1)
        .Select(x => positions.Select(pos => pos > x ? pos - x : x - pos)
            .Sum(fuelSum))
        .Min();
}

Console.WriteLine("Part 1");
Console.WriteLine(CalculateFuel(i => i));

Console.WriteLine("Part 2");
Console.WriteLine(CalculateFuel(i => Enumerable.Range(1, i).Sum()));
