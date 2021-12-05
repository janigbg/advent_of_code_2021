const string file = @"..\..\..\..\input\4a.txt";

var items = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .ToList();

var numbers = items[0]
    .Split(',')
    .Select(int.Parse)
    .ToList();

var boards = items
    .Skip(1)
    .Chunk(5)
    .Select(b =>
        b.SelectMany(s =>
            s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray())
    .Select(b => new Board(b))
    .ToList();

var notWon = boards;
for (var i = 0; i < numbers.Count; i++)
{
    if (notWon.Count() == 1)
    {
        var loser = notWon.Single();
        if (loser.Add(numbers[i]))
        {
            Console.WriteLine(loser.Score(numbers.Take(i + 1)));
            break;
        }
    }
    else
    {
        notWon = notWon.Where(b => !b.Add(numbers[i])).ToList();
    }
}



