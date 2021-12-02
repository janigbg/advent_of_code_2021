const string file = @"..\..\..\..\input\1a.txt";
const int windowSize = 3;

var result = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .Select(int.Parse)
    .Aggregate<int, (int count, IEnumerable<int> prev)>(
        (0, new List<int>()),
        (acc, current) => (acc.count + (acc.prev.Count() >= windowSize && current > acc.prev.ElementAt(windowSize - 1) ? 1 : 0), acc.prev.Prepend(current)))
    .count;

Console.WriteLine(result);
