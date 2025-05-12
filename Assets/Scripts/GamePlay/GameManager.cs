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
/// 틱택토 게임을 진행한다. => 비즈니스 로직 => 핵심 모듈
/// </summary>
public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    private SquareState[,] board = new SquareState[3, 3];

    private NetworkVariable<SquareState> currentTurnState = new();
    public SquareState localPlayerType = SquareState.None;

    public event Action<int, int, SquareState> OnBoardChaged;
    public event Action<GameOverState> OnGameEnded;
    public event Action<SquareState> ChangeHudUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        currentTurnState.OnValueChanged += ChangeValue;

        NetworkManager.Singleton.OnConnectionEvent += (networkManager, connectionEventData) =>
        {
            Logger.Info($"Client {connectionEventData.ClientId}, {connectionEventData.EventType}");
            if (NetworkManager.ConnectedClients.Count == 2)
            {
                if (IsHost)
                {
                    localPlayerType = SquareState.Cross;
                    currentTurnState.Value = SquareState.Cross;
                }
                else if (IsClient)
                {
                    localPlayerType = SquareState.Circle;
                }
            }
        };
    }

    public void PlayMarker(int x, int y)
    {
        if (localPlayerType == currentTurnState.Value)
        {
            ReqValidateRpc(x, y, localPlayerType);
        }

        /*if (board[y, x] == SquareState.None && TestGameOver() == GameOverState.NotOver) // 칸이 선택된적 없을때
        {
            if (currentTurnState.Value == SquareState.Cross) // x 차례
            {
                board[y, x] = SquareState.Cross;

                OnBoardChaged?.Invoke(x, y, SquareState.Cross);
            }
            else if (currentTurnState.Value == SquareState.Circle) // ㅇ 차례
            {
                board[y, x] = SquareState.Circle;

                OnBoardChaged?.Invoke(x, y, SquareState.Circle);
            }

            ChangeHudUI?.Invoke(currentTurnState.Value);

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
        }*/
    }

    /// <summary>
    /// 게임 상태 반환 함수
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

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[y, x] == SquareState.None)
                {
                    return GameOverState.NotOver;
                }
            }
        }

        return GameOverState.Tie;
    }

    [Rpc(SendTo.Server)]
    public void ReqValidateRpc(int x, int y, SquareState state)
    {
        if(false == IsValidPLayerMarker(x, y, state))
        {
            return;
        }

        ChangeBoradStateRpc(x, y, state);

        if (currentTurnState.Value == SquareState.Cross)
        {
            currentTurnState.Value = SquareState.Circle;
        }
        else if (currentTurnState.Value == SquareState.Circle)
        {
            currentTurnState.Value = SquareState.Cross;
        }
    }

    [Rpc(SendTo.Everyone)]
    public void ChangeBoradStateRpc(int x, int y, SquareState state)
    {
        board[y, x] = state;
        OnBoardChaged?.Invoke(x, y, state);
    }

    private bool IsValidPLayerMarker(int x, int y, SquareState state)
    {
        return TestGameOver() == GameOverState.NotOver &&
            state == currentTurnState.Value &&
            board[y, x] == SquareState.None;
    }

    public void ChangeValue(SquareState pre, SquareState changeV)
    {
        Logger.Info($"{pre} => {changeV}");

        ChangeHudUI?.Invoke(changeV);
    }
}
