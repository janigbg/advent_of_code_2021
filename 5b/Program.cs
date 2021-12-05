const string file = @"..\..\..\..\input\5a.txt";

bool IsHorisontal(((int x, int y) p1, (int x, int y) p2) line) =>
    line.p1.y == line.p2.y;

bool IsVertical(((int x, int y) p1, (int x, int y) p2) line) =>
    line.p1.x == line.p2.x;

var items = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file))
    .Select(line =>
    {
        var items = line.Split(' ');
        var p1 = items[0].Split(',')
            .Select(int.Parse);
        (int x1, int y1) = (p1.ElementAt(0), p1.ElementAt(1));
        var p2 = items[2].Split(',')
            .Select(int.Parse);
        (int x2, int y2) = (p2.ElementAt(0), p2.ElementAt(1));
        return (p1: (x: x1, y: y1), p2: (x: x2, y: y2));
    })
    .Select(p => p.p1.x > p.p2.x || (p.p1.y > p.p2.y && IsVertical(p))
        ? (p1: p.p2, p2: p.p1)
        : (p1: p.p1, p2: p.p2))
    .SelectMany(line =>
    {
        var points = (IsHorisontal(line), IsVertical(line), line.p1.y < line.p2.y) switch
        {
            (true, _, _) => Enumerable.Range(line.p1.x, line.p2.x - line.p1.x + 1)
                .Select(x => (x: x, y: line.p1.y)),
            (_, true, _) => Enumerable.Range(line.p1.y, line.p2.y - line.p1.y + 1)
                .Select(y => (x: line.p1.x, y: y)),
            (false, false, true) => Enumerable.Range(0, line.p2.x - line.p1.x + 1)
                .Select(i => (x: line.p1.x + i, y: line.p1.y + i)),
            (false, false, false) => Enumerable.Range(0, line.p2.x - line.p1.x + 1)
                .Select(i => (x: line.p1.x + i, y: line.p1.y - i)),
        };

        return points;
    })
    .Aggregate(
        new Dictionary<(int x, int y), int>(),
        (dict, p) =>
        {
            if (dict.ContainsKey(p))
            {
                dict[p] += 1;
            }
            else
            {
                dict.Add(p, 1);
            }

            return dict;
        });

Console.WriteLine(items.Values.Count(v => v > 1));

