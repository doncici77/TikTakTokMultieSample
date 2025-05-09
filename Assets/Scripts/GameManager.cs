using System;
using UnityEngine;
using Unity.Netcode;

public enum SquareState
{
    None,
    Cross,
    Circle
}

public enum GameOverState
{
    NotOver,
    Cross,
    Circle,
    Tie
}

/// <summary>
/// ƽ���� ������ �����Ѵ�. => ����Ͻ� ���� => �ٽ� ���
/// </summary>
public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    private SquareState[,] board = new SquareState[3, 3];
    private SquareState currentTurnState = SquareState.Cross;

    public event Action<int, int, SquareState> OnBoardChaged;
    public event Action<GameOverState> OnGameEnded;
    public event Action<SquareState> ChangeHudUI;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }
    }


    public void PlayMarker(int x, int y)
    {
        if (board[y, x] == SquareState.None && TestGameOver() == GameOverState.NotOver) // ĭ�� ���õ��� ������
        {
            if (currentTurnState == SquareState.Cross) // x ����
            {
                board[y, x] = SquareState.Cross;

                OnBoardChaged?.Invoke(x, y, SquareState.Cross);

                currentTurnState = SquareState.Circle;
            }
            else if (currentTurnState == SquareState.Circle) // �� ����
            {
                board[y, x] = SquareState.Circle;

                OnBoardChaged?.Invoke(x, y, SquareState.Circle);

                currentTurnState = SquareState.Cross;
            }

            ChangeHudUI?.Invoke(currentTurnState);

            if (TestGameOver() == GameOverState.Cross || TestGameOver() == GameOverState.Circle)
            {
                Logger.Info($"{TestGameOver()} is Winner!");
                OnGameEnded?.Invoke(TestGameOver());
            }
            else if (TestGameOver() == GameOverState.Tie)
            {
                Logger.Info($"{TestGameOver()}!");
                OnGameEnded?.Invoke(TestGameOver());
            }
        }
    }

    /// <summary>
    /// ���� ���� ��ȯ �Լ�
    /// </summary>
    /// <returns></returns>
    GameOverState TestGameOver()
    {
        for (int line = 0; line < 3; line++)
        {
            if (board[line, 0] == board[line, 1] &&
                board[line, 1] == board[line, 2])
            {
                if (board[line, 0] == SquareState.Cross)
                {
                    return GameOverState.Cross;
                }
                else if (board[line, 0] == SquareState.Circle)
                {
                    return GameOverState.Circle;
                }
            }
            else if (board[0, line] == board[1, line] &&
                board[1, line] == board[2, line])
            {
                if (board[0, line] == SquareState.Cross)
                {
                    return GameOverState.Cross;
                }
                else if (board[0, line] == SquareState.Circle)
                {
                    return GameOverState.Circle;
                }
            }
        }

        if (board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2])
        {
            if (board[0, 0] == SquareState.Cross)
            {
                return GameOverState.Cross;
            }
            else if (board[0, 0] == SquareState.Circle)
            {
                return GameOverState.Circle;
            }
        }
        else if (board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0])
        {
            if (board[0, 2] == SquareState.Cross)
            {
                return GameOverState.Cross;
            }
            else if (board[0, 2] == SquareState.Circle)
            {
                return GameOverState.Circle;
            }
        }

        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                if (board[y, x] == SquareState.None)
                {
                    return GameOverState.NotOver;
                }
            }
        }

        return GameOverState.Tie;
    }
}
