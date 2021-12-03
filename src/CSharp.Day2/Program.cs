
var lines = File.ReadAllLines("input.txt")
    .Select(line => line.Split(' '))
    .Select(x => new { Direction = x[0], Distance = int.Parse(x[1]) })
    .ToList();

TStrategy Run<TStrategy>() where TStrategy: ISubmarine<TStrategy>, new()
    => lines.Aggregate(new TStrategy(), (acc, x) => acc.Move(x.Direction, x.Distance));

void Part1()
{
    var position = Run<Position>();
    Console.WriteLine($"Horizontal position of {position.Horizontal} and a depth of {position.Depth}");
}

void Part2()
{
    var position = Run<Aim>();
    Console.WriteLine($"Horizontal position of {position.Horizontal} and a depth of {position.Depth}");
}

Part1();
Part2();

interface ISubmarine<out TStrategy>
{
    TStrategy Move(string direction, int distance);
}

record Position : ISubmarine<Position>
{
    public Position Move(string direction, int distance) =>
        direction switch
        {
            "forward" => this with { Horizontal = Horizontal + distance },
            "down" => this with { Depth = Depth + distance },
            "up" => this with { Depth = Depth - distance },
            _ => throw new Exception("Unknown direction")
        };

    public int Horizontal { get; init; }
    public int Depth { get; init; }
}

record Aim : ISubmarine<Aim>
{
    public Aim Move(string direction, int distance) =>
        direction switch
        {
            "forward" => this with { Horizontal = Horizontal + distance, Depth = Depth + AimUnits * distance },
            "down" => this with { AimUnits = AimUnits + distance },
            "up" => this with { AimUnits = AimUnits - distance },
            _ => throw new Exception("Unknown direction")
        };

    public int Horizontal { get; init; }
    public int Depth { get; init; }
    public int AimUnits { get; init; }
}

 