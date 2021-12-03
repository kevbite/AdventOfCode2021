// See https://aka.ms/new-console-template for more information

var lines = File.ReadAllLines("input.txt")
    .ToArray();
    
var gammaRate = new string(Enumerable.Range(0,  lines[0].Length)
    .Select(index => lines.GroupBy(x => x[index])
        .Select(group => new {
            Count = group.Count(),
            Value = group.Key
        })
        .OrderByDescending(x => x.Count)
        .ThenByDescending(x => x.Value)
        .First()
        .Value)
    .ToArray());

var epsilonRate = new string(gammaRate.Select(x => x == '1' ? '0' : '1').ToArray());

Console.WriteLine("The gamma rate is " + gammaRate);
Console.WriteLine("The epsilon rate is " + epsilonRate);
        
Console.WriteLine(Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate,2));

var possibleOxygenGeneratorRatings = lines;
var possibleCO2scrubberRatings = lines;
for(var i =0; i <= gammaRate.Length; i++)
{
    if (possibleOxygenGeneratorRatings.Length != 1)
    {
        var bits = possibleOxygenGeneratorRatings.Select(x => x[i])
            .GroupBy(x => x)
            .Select(group => new
            {
                Count = group.Count(),
                Value = group.Key
            })
            .OrderByDescending(x => x.Count)
            .ThenByDescending(x => x.Value)
            .ToArray();
        
        possibleOxygenGeneratorRatings = possibleOxygenGeneratorRatings
            .Where(x => x[i] == bits.First().Value).ToArray();
    }
    
    if (possibleCO2scrubberRatings.Length != 1)
    {
        var bits = possibleCO2scrubberRatings.Select(x => x[i])
            .GroupBy(x => x)
            .Select(group => new
            {
                Count = group.Count(),
                Value = group.Key
            })
            .OrderByDescending(x => x.Count)
            .ThenByDescending(x => x.Value)
            .ToArray();
        
        possibleCO2scrubberRatings = possibleCO2scrubberRatings
            .Where(x => x[i] == bits.Last().Value).ToArray();
    }
}

Console.WriteLine("The Oxygen Generator rate is " + possibleOxygenGeneratorRatings.Single());
Console.WriteLine("The CO2 scrubber  rate is " + possibleCO2scrubberRatings.Single());
Console.WriteLine(Convert.ToInt32(possibleOxygenGeneratorRatings.Single(), 2) * Convert.ToInt32(possibleCO2scrubberRatings.Single(),2));
