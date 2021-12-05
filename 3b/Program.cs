const string file = @"..\..\..\..\input\3a.txt";

var items = (await File.ReadAllLinesAsync(args?.Length > 0 ? args[0] : file)).ToList();

int valueToMatch(IEnumerable<int> values, int bit, bool mostCommon)
{
    var bVal = 1 << bit;
    (int zeroes, int ones) = (0, 0);
    values.ToList().ForEach(value =>
    {
        if ((value & bVal) > 0)
        {
            ones++;
        }
        else
        {
            zeroes++;
        }
    });

    return mostCommon
        ? ones >= zeroes ? bVal : 0
        : zeroes <= ones ? 0 : bVal;
};

int findSingleMatch(IEnumerable<int> values, int numberOfBits, bool mostCommon)
{
    var selected = values.ToList();
    for (int i = numberOfBits - 1; i >= 0; i--)
    {
        var bVal = 1 << i;
        selected = selected.Where(item => (item & bVal) == valueToMatch(selected, i, mostCommon)).ToList();
        if (selected.Count() == 1)
        {
            break;
        }
    }

    return selected.First();
}

var values = items.Select(item =>
{
    var x = 0;
    for (int i = 0; i < item.Length; i++)
    {
        x <<= 1;
        if (item[i] == '1')
        {
            x += 1;
        }
    }
    return x;
});

var oxy = findSingleMatch(values, items.First().Length, true);
var co2 = findSingleMatch(values, items.First().Length, false);

Console.WriteLine(oxy * co2);