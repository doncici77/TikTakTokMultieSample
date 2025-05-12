using UnityEngine;

/// <summary>
/// ������ ���¸� ���ø����̼ǿ� ����Ѵ�.
/// </summary>
public class GameVisualManager : MonoBehaviour
{
    [SerializeField] private GameObject crossPrefab;
    [SerializeField] private GameObject circlePrefab;

    private void Start()
    {
        GameManager.Instance.OnBoardChaged += CreateMarker;
    }

    private void CreateMarker(int x, int y, SquareState squareState)
    {
        switch(squareState)
        {
            case SquareState.Cross:
                Instantiate(crossPrefab, GetWorldPositionFromCoodinate(x, y), Quaternion.identity);
                break;
            case SquareState.Circle:
                Instantiate(circlePrefab, GetWorldPositionFromCoodinate(x, y), Quaternion.identity);
                break;
            default:
                Logger.Error("CreateMarker : �����Ҷ� ������ ������");
                break;
        }
    }

    private Vector2 GetWorldPositionFromCoodinate(int x, int y)
    {
        int worldX = -3 + 3 * x;
        int worldY = 3 - 3 * y;

        return new Vector2(worldX, worldY);
    }
}
