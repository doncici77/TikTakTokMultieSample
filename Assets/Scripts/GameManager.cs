using UnityEngine;

/// <summary>
/// 틱택토 게임을 진행한다. => 비즈니스 로직 => 핵심 모듈
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    public void ProcessInput(int x, int y)
    {
        Logger.Info("ProcessInput() Called");
    }
}
