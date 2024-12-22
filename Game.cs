using System.Linq;

namespace TicTacToe;

public static class Game
{
    private static char[] field = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    public static char CurrentPlayer { get; private set; } = 'X';

    public static bool IsTaken(int cellIndex)
    {
        return field[cellIndex] != ' ';
    }

    public static void Turn(int cellIndex)
    {
        field[cellIndex] = CurrentPlayer;
    }

    public static void ChangePlayer()
    {
        CurrentPlayer = (CurrentPlayer == 'X' ? 'O' : 'X');
    }

    public static Status GetGameStatus()
    {
        // Перевірка горизонтальних ліній
        for (int i = 0; i < 3; i++)
        {
            if (field[i * 3] != ' ' && field[i * 3] == field[i * 3 + 1] && field[i * 3] == field[i * 3 + 2])
            {
                return Status.HasWinner;
            }
        }

        // Перевірка вертикальних ліній
        for (int i = 0; i < 3; i++)
        {
            if (field[i] != ' ' && field[i] == field[i + 3] && field[i] == field[i + 6])
            {
                return Status.HasWinner;
            }
        }

        // Перевірка діагоналей
        if (field[0] != ' ' && field[0] == field[4] && field[0] == field[8])
        {
            return Status.HasWinner;
        }
        if (field[2] != ' ' && field[2] == field[4] && field[2] == field[6])
        {
            return Status.HasWinner;
        }

        // Якщо немає переможця але всі комірки зайняти (немає пустих) - нічия
        if (!field.Contains(' '))
            return Status.Draw;

        return Status.NoWinner;
    }

    // Очищаємо поле для нової гри
    public static void ResetField()
    {
        field = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    }

    public enum Status
    {
        NoWinner,
        HasWinner,
        Draw
    }
}
