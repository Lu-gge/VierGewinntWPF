using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VierGewinntWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            const int COLUMNS = 7;
            const int ROWS = 6;
            UiGrid.ShowGridLines = true;

            for (int i = 0; i < ROWS; i++)
            {
                UiGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80, GridUnitType.Star) });
            }
            for (int i = 0; i < COLUMNS; i++)
            {
                UiGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80, GridUnitType.Star) });
            }

        }

        private static string Spieler1 { get; set; } = "Rot";
        private static string Spieler2 { get; set; } = "Blau";
        private static int[,] Spielfeld = new int[6, 7];
        private static bool ForceNewGame = true;

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            UiGrid.Children.Clear();
            Spieler1 = RotName.Text;
            Spieler2 = BlauName.Text;
            Spielfeld = new int[6, 7];
            ForceNewGame = false;

            for (int i = 0; i < Spielfeld.GetLength(0); i++)
            {
                for (int j = 0; j < Spielfeld.GetLength(1); j++)
                {
                    Spielfeld[i, j] = 0;
                }
            }

            foreach (Button item in Can.Children)
            {
                item.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void Auswahl_Click(object sender, RoutedEventArgs e)
        {
            if (ForceNewGame)
            {
                MessageBox.Show($"Starte ein neues Spiel!", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Button bt = (Button)sender;
            TextBlock tb = new TextBlock();

            int column = int.Parse(bt.Tag.ToString()!);
            int row = 5;
            bool message = true;

            while (row >= 0)
            {
                if (Spielfeld[row, column] == 0)
                {
                    Grid.SetColumn(tb, int.Parse(bt.Tag.ToString()!));
                    Grid.SetRow(tb, row);
                    message = false;
                    break;
                }
                row--;
            }

            if (!message)
            {
                if ((bt.Background as SolidColorBrush)?.Color == Colors.Red)
                {
                    foreach (Button item in Can.Children)
                    {
                        item.Background = new SolidColorBrush(Colors.Blue);
                    }
                    tb.Background = new SolidColorBrush(Colors.Red);
                    Spielfeld[row, column] = 1;
                }
                else
                {
                    foreach (Button item in Can.Children)
                    {
                        item.Background = new SolidColorBrush(Colors.Red);
                    }
                    tb.Background = new SolidColorBrush(Colors.Blue);
                    Spielfeld[row, column] = 2;
                }
                UiGrid.Children.Add(tb);

                int gewinner = GewinnerErmitteln();
                if (gewinner == 1)
                {
                    foreach (Button item in Can.Children)
                    {
                        item.Background = new SolidColorBrush(Colors.Red);
                    }
                    MessageBox.Show($"{Spieler1} gewinnt!", "Gratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    ForceNewGame = true;
                    return;
                }
                else if (gewinner == 2)
                {
                    foreach (Button item in Can.Children)
                    {
                        item.Background = new SolidColorBrush(Colors.Blue);
                    }
                    MessageBox.Show($"{Spieler2} gewinnt!", "Gratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    ForceNewGame = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("In dieser Spalte kann kein weiteres Feld gesetzt werden!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            bool unentschieden = true;
            for (int i = 0; i < Spielfeld.GetLength(0); i++)
            {
                for (int j = 0; j < Spielfeld.GetLength(1); j++)
                {
                    if (Spielfeld[i, j] == 0)
                    {
                        unentschieden = false;
                    }
                }
            }

            if (unentschieden)
            {
                MessageBox.Show("Es ist ein Unentschieden!", "Gratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                ForceNewGame = true;
                return;
            }
        }

        private int GewinnerErmitteln()
        {
            for (int row = 0; row < Spielfeld.GetLength(0); row++)
            {
                for (int col = 0; col < Spielfeld.GetLength(1); col++)
                {
                    int spieler = Spielfeld[row, col];
                    if (spieler == 0)
                    {
                        continue;
                    }

                    if (col <= Spielfeld.GetLength(1) - 4 && Spielfeld[row, col + 1] == spieler && Spielfeld[row, col + 2] == spieler && Spielfeld[row, col + 3] == spieler)
                    {
                        Siegersteine("Waagerecht", spieler, row, col);
                        return spieler;
                    }

                    if (row <= Spielfeld.GetLength(0) - 4 && Spielfeld[row + 1, col] == spieler && Spielfeld[row + 2, col] == spieler && Spielfeld[row + 3, col] == spieler)
                    {
                        Siegersteine("Senkrecht", spieler, row, col);
                        return spieler;
                    }

                    if (row <= Spielfeld.GetLength(0) - 4 && col <= Spielfeld.GetLength(1) - 4 && Spielfeld[row + 1, col + 1] == spieler && Spielfeld[row + 2, col + 2] == spieler && Spielfeld[row + 3, col + 3] == spieler)
                    {
                        Siegersteine("Diagonal_Rechts", spieler, row, col);
                        return spieler;
                    }

                    if (row <= Spielfeld.GetLength(0) - 4 && col >= Spielfeld.GetLength(1) - 4 && Spielfeld[row + 1, col - 1] == spieler && Spielfeld[row + 2, col - 2] == spieler && Spielfeld[row + 3, col - 3] == spieler)
                    {
                        Siegersteine("Diagonal_Links", spieler, row, col);
                        return spieler;
                    }
                }
            }
            return 0;
        }

        private void Siegersteine(string reihe, int spieler, int row, int col)
        {
            for (int i = 0; i < 4; i++)
            {
                TextBlock tb = new();

                switch (reihe)
                {
                    case "Waagerecht":
                        Grid.SetRow(tb, row);
                        Grid.SetColumn(tb, col + i);
                        break;
                    case "Senkrecht":
                        Grid.SetRow(tb, row + i);
                        Grid.SetColumn(tb, col);
                        break;
                    case "Diagonal_Rechts":
                        Grid.SetRow(tb, row + i);
                        Grid.SetColumn(tb, col + i);
                        break;
                    case "Diagonal_Links":
                        Grid.SetRow(tb, row + i);
                        Grid.SetColumn(tb, col - i);
                        break;
                }

                tb.Text = "*";
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.FontSize = 40;
                switch (spieler)
                {
                    case 1: tb.Background = new SolidColorBrush(Colors.Red); break;
                    case 2: tb.Background = new SolidColorBrush(Colors.Blue); break;
                }
                UiGrid.Children.Add(tb);
            }
        }

    }
}