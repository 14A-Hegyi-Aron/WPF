using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace sakk
{
    public abstract class Figure
    {
        public Image Image { get; set; }
        public Color Color { get; }
        public WrapPanel? WrapPanel { get; set; }

        public event EventHandler Activated;

        private readonly Grid grid;
        private bool active = false;
        public int Row
        {
            get
            {
                if (WrapPanel == null)
                    return 0;
                return Grid.GetRow(WrapPanel);
            }
        }
        public int Col
        {
            get
            {
                if (WrapPanel == null)
                    return 0;
                return Grid.GetColumn(WrapPanel);
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                if (this.WrapPanel == null)
                    return;
                active = value;
                if (!active)
                {
                    this.WrapPanel.Background = (Row + Col) % 2 == 0 ? Brushes.DodgerBlue : Brushes.DarkBlue;
                    foreach (var move in this.CanMove())
                    {
                        var panel = this.getPanel(move.Item1, move.Item2);
                        if (panel != null)
                        {
                            panel.Background = (move.Item1 + move.Item2) % 2 == 0 ? Brushes.DodgerBlue : Brushes.DarkBlue;
                        }
                    }
                }
                else
                {
                    if (Activated != null)
                        this.Activated(this, new EventArgs());
                    this.WrapPanel.Background = (Row + Col) % 2 == 0 ? Brushes.Magenta : Brushes.DarkMagenta;
                    foreach (var move in this.CanMove())
                    {
                        var panel = this.getPanel(move.Item1, move.Item2);
                        if (panel != null)
                        {
                            panel.Background = (move.Item1 + move.Item2) % 2 == 0 ? Brushes.Magenta : Brushes.DarkMagenta;
                        }
                    }
                }
            }
        }

        public Figure(Color color, int row, int col, Grid grid)
        {
            this.grid = grid;
            this.Color = color;
        }
        /*public string Position
        {
            get
            {
                return string.Format($"{(char)(col + 64)}{9 - row}");
            }
            set
            {
                if (value.Length != 2)
                    throw new ArgumentException("Invalid length of value");

                if (value[0] < 'A' || value[0] > 'H')
                    throw new ArgumentException("Invalid column");

                if (value[0] < '1' || value[0] > '8')
                    throw new ArgumentException("Invalid row");

                row = 9 - (value[1] - '0');
                col = value[0] - 64;
            }
        }*/
        public virtual void Move(int row, int col)
        {
            if (this.WrapPanel != null)
                this.WrapPanel.Children.Remove(this.Image);
            this.WrapPanel = getPanel(row, col);
            if (this.WrapPanel != null)
                this.WrapPanel.Children.Add(this.Image);
        }

        private WrapPanel? getPanel(int row, int col)
        {
            foreach (var item in grid.Children)
            {
                if (item is WrapPanel)
                {
                    var panel = item as WrapPanel;
                    if (Grid.GetRow(panel) == row && Grid.GetColumn(panel) == col)
                        return panel;
                }
            }
            return null;
        }

        public abstract List<Tuple<int, int>> CanMove();

        public void LoadImage(Uri uri, int row, int col)
        {
            this.Image = new Image()
            {
                Source = new BitmapImage(uri)
            };
            this.Move(row, col);

            this.Image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Active = !this.Active;
        }
    }
}
