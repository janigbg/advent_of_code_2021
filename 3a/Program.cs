const string file = @"..\..\..\..\input\3a.txt";

var dict = new Dictionary<int, (int, int)>();

(await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .ToList()
    .ForEach(line =>
    {
        for (int i = 0; i < line.Length; i++)
        {
            var (zeroes, ones) = dict.GetValueOrDefault(i);
            if (line[i] == '0')
            {
                zeroes += 1;
            }
            else
            {
                ones += 1;
            }

            dict[i] = (zeroes, ones);
        }
    });

var list = dict.ToList();
list.Sort((a, b) => a.Key < b.Key ? -1 : a.Key == b.Key ? 0 : 1);
var gamma = list
    .Aggregate(0, (val, curr) =>
    {
        var bit = curr.Value.Item1 > curr.Value.Item2 ? 0 : 1;
        val = val << 1;
        return val + bit;
    });

var epsilon = list
    .Aggregate(0, (val, curr) =>
    {
        var bit = curr.Value.Item1 < curr.Value.Item2 ? 0 : 1;
        val = val << 1;
        return val + bit;
    });

Console.WriteLine(gamma * epsilon);