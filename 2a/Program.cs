const string file = @"..\..\..\..\input\2a.txt";

var result = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .Select(line =>
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return (cmd: parts[0], num: int.Parse(parts[1]));
    })
    .Aggregate<(string cmd, int num), (int forward, int depth)>(
        (0, 0),
        (current, command) => command switch
        {
            ("forward", _) => (current.forward + command.num, current.depth),
            ("down", _) => (current.forward, current.depth + command.num),
            ("up", _) => (current.forward, current.depth - command.num),
            _ => current
        }
    );

Console.WriteLine(result.forward * result.depth);
