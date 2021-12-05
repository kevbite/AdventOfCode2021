
var lines = File.ReadAllLines("input.txt")
    .Select(x =>
    {
        var split = x.Split(" -> ");
        var pos1 = split[0].Split(',').Select(int.Parse).ToArray();
        var pos2 = split[1].Split(',').Select(int.Parse).ToArray();
        return new Line(pos1[0], pos1[1], pos2[0], pos2[1]);
    })
    .ToArray();


var count = lines
    .SelectMany(x => x.GetAllHorizontalAndVerticalPositions())
    .GroupBy(x => x)
    .Count(x => x.Count() > 1);

Console.WriteLine(count);
Console.WriteLine();

var groupBy = lines
    .SelectMany(x => x.GetPositions())
    .GroupBy(x => x).ToList();
var count2 = groupBy
    .Count(x => x.Count() > 1);

Console.WriteLine(count2);
Console.WriteLine();

var lineTest = new Line(9,7,7,9);
foreach (var thing in lineTest.GetPositions())
{
    Console.WriteLine(thing);
}

record Line(int X1, int Y1, int X2, int Y2)
{
    public IEnumerable<Position> GetAllHorizontalAndVerticalPositions()
    {
        if (X1 != X2 && Y1 != Y2)
            return Enumerable.Empty<Position>();
        
        return Enumerable.Range(Math.Min(X1, X2), Math.Max(X1, X2) - Math.Min(X1, X2) + 1)
            .Select(x => new Position(x, Y1))
            .Union(Enumerable.Range(Math.Min(Y1, Y2), Math.Max(Y1, Y2) - Math.Min(Y1, Y2) + 1)
                .Select(y => new Position(X1, y)));
    } 
    public IEnumerable<Position> GetPositions()
    {
        var distanceX = Math.Max(X1, X2) - Math.Min(X1, X2);
        var distanceY = Math.Max(Y1, Y2) - Math.Min(Y1, Y2);
        
        if (distanceX == distanceY)
        {
            var currentX = X1;
            var currentY = Y1;
            
            var incX = X1 < X2 ? 1 : -1;
            var incY = Y1 < Y2 ? 1 : -1;
            
            yield return new Position(currentX, currentY);

            while(currentX != X2 || currentY != Y2)
            {
                currentX += incX;
                currentY += incY;
                yield return new Position(currentX, currentY);
            }
        }
        else
        {
            foreach (var allHorizontalAndVerticalPosition in GetAllHorizontalAndVerticalPositions())
                yield return allHorizontalAndVerticalPosition;
        }
    } 
}

record Position(int X, int Y);