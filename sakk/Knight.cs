using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sakk
{
    public class Knight : Figure
    {
        public Knight(Color color, int row, int col, Grid grid) : base(color, row, col, grid)
        {
            Uri uri = new($"{Directory.GetCurrentDirectory()}/figures/{color}/knight.png");
            LoadImage(uri, row, col);
        }
        public override List<Tuple<int, int>> CanMove()
        {
            List<Tuple<int, int>> result = new();

            List<Tuple<int, int>> offsets = new()
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, -2),
                new Tuple<int, int>(-1, 2),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-2, -1),
            };
            foreach (var offset in offsets)
                if (Row + offset.Item1 >= 1 && Row + offset.Item1 <= 8 && Col + offset.Item2 >= 1 && Col + offset.Item2 <= 8)
                    result.Add(new(Row + offset.Item1, Col + offset.Item2));

            return result;
        }
    }
}