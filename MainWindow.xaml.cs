using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe;

public partial class MainWindow : Window
{
    // Переменные для счета
    private int playerXWins = 0;
    private int playerOWins = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    public void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Button pressedButton = (Button)sender;
        int cellIndex = Convert.ToInt32(pressedButton.Tag);

        if (Game.IsTaken(cellIndex))
            return;

        Game.Turn(cellIndex);
        pressedButton.Content = Game.CurrentPlayer;

        Game.Status gameStatus = Game.GetGameStatus();

        switch (gameStatus)
        {
            case Game.Status.HasWinner:
                MessageBox.Show($"Player '{Game.CurrentPlayer}' wins!");
                UpdateScore(Game.CurrentPlayer);
                ResetGame();
                break;

            case Game.Status.Draw:
                MessageBox.Show("It's a draw!");
                ResetGame();
                break;
        }

        Game.ChangePlayer();
    }

    // Очищаємо всі кнопки та поля для нової гри
    private void ResetGame()
    {
        foreach (var button in FindVisualChildren<Button>(this))
        {
            button.Content = null;
        }

        Game.ResetField();
    }

    // Обновляем счет игрока
    private void UpdateScore(char player)
    {
        if (player == 'X')
        {
            playerXWins++;
            PlayerXScore.Text = $"{PlayerXName.Text}: {playerXWins}";
        }
        else if (player == 'O')
        {
            playerOWins++;
            PlayerOScore.Text = $"{PlayerOName.Text}: {playerOWins}";
        }
    }

    // Метод для знаходження всіх елементів типу Button в інтерфейсі
    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }
    }
}
