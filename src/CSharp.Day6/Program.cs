var daysLeft = File.ReadAllLines("input.txt")
    .Single()
    .Split(',')
    .Select(int.Parse)
    .ToArray();

Console.WriteLine("Part 1: " + Sum(daysLeft, 80));

Console.WriteLine("Part 2: " + Sum(daysLeft, 256));

long Sum(IEnumerable<int> enumerable, int days)
{
    var initial = enumerable.GroupBy(x => x)
        .ToDictionary(x => x.Key, x => x.Count());
    var daysCountMap = Enumerable.Range(0, 9)
        .Select(x => initial.TryGetValue(x, out var count) switch
        {
            true => count,
            false => 0L
        })
        .ToArray();

    for (var day = 0; day < days; day++)
    {
        var zeroCount = daysCountMap[0];
        Array.Copy(daysCountMap, 1, daysCountMap, 0, daysCountMap.Length - 1);

        daysCountMap[8] = zeroCount;
        daysCountMap[6] += zeroCount;
    }

    var l = daysCountMap.Sum(x => x);
    return l;
}
