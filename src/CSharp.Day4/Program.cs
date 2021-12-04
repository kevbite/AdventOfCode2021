var lines = File.ReadAllLines("example.txt")
    .ToArray();

var numbers = lines.First()
    .Split(',')
    .Select(int.Parse)
    .ToArray();


var boards = lines.Skip(1)
    .Chunk(6)
    .Select(x => new BingoBoard(
        x.Skip(1).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray()))
    .ToList();

void Part1()
{
    var winningRound = Enumerable.Range(1, numbers.Length)
        .Select(i => numbers.Take(i).ToArray())
        .Select(currentRound => new
        {
            Winner = boards.FirstOrDefault(x => x.IsWinner(currentRound)),
            Round = currentRound
        })
        .FirstOrDefault(x => x.Winner is { });

    PrintScore(winningRound.Winner, winningRound.Round);
}

void Part2()
{
    for (var i = 1; i < numbers.Length; i++)
    {
        var currentRound = numbers.Take(i).ToArray();

        if (boards.All(x => x.IsWinner(currentRound)))
        {
            var previousRound = currentRound.Take(i - 1).ToArray();
            var lastBoard = boards.Single(x => !x.IsWinner(previousRound));

            PrintScore(lastBoard, currentRound);
            break;
        }
    }
}

void PrintScore(BingoBoard bingoBoard, int[] ints)
{
    var unmarkedNumbersSum = bingoBoard.rows.SelectMany(x => x)
        .Except(ints)
        .Sum();
    Console.WriteLine(unmarkedNumbersSum);
    Console.WriteLine(ints.Last());
    Console.WriteLine(unmarkedNumbersSum * ints.Last());
}

Part1();
Part2();

record BingoBoard(int[][] rows)
{
    public bool IsWinner(int[] numbers)
    {
        var columns = Enumerable.Range(0, 5)
            .Select(i => rows.Select(x => x[i])
                .ToArray());
        if (rows
            .Concat(columns)
            .Any(x => x.All(numbers.Contains)))
        {
            return true;
        }

        return false;
    }
}