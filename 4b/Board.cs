internal class Board
{
    public Dictionary<int, (int row, int col)> Map { get; } = new();
    public int[] Rows { get; } = new int[5];
    public int[] Cols { get; } = new int[5];

    public Board(IEnumerable<int> board)
    {
        var b = board.ToArray();
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                Map[b[row*5+col]] = (row, col);
            }
        }
    }

    public bool Add(int number)
    {
        if (!Map.ContainsKey(number))
        {
            return false;
        }

        (int row, int col) = Map[number];
        Rows[row]++;
        Cols[col]++;

        return Rows[row] == 5 || Cols[col] == 5;
    }

    public int Score(IEnumerable<int> numbers) =>
        Map.Keys.Except(numbers).Sum() * numbers.Last();
}
