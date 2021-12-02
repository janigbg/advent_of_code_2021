const string file = @"..\..\..\..\input\1a.txt";

var result = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .Select(int.Parse)
    .Aggregate<int, (int count, int? prev)>((0, null), (acc, current) => (acc.count + (current > acc.prev ? 1 : 0), current))
    .count;

Console.WriteLine(result);
