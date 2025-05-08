using UnityEngine;

/// <summary>
/// ƽ���� ������ �����Ѵ�. => ����Ͻ� ���� => �ٽ� ���
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
