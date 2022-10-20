using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace sakk
{
    public class Pawn : Figure
    {
        public Pawn(Color color, int row, int col, Grid grid) : base(color, row, col, grid)
        {
            Uri uri = new($"{Directory.GetCurrentDirectory()}/figures/{color}/pawn.png");
            LoadImage(uri, row, col);
        }
        public override List<Tuple<int, int>> CanMove()
        {
            List<Tuple<int, int>> result = new();
            if (this.Color == Color.White)
            {
                if (Row != 1)
                {
                    result.Add(new(Row - 1, Col));
                    if (Row == 7)
                        result.Add(new(5, Col));
                }
            }
            else
            {
                if (Row != 8)
                {
                    result.Add(new(Row + 1, Col));
                    if (Row == 2)
                        result.Add(new(4, Col));
                }
            }
            return result;
        }
    }
}
