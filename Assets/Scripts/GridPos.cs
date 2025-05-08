using UnityEngine;

public class GridPos : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    private void OnMouseDown()
    {
        Logger.Info($"({x}, {y})좌표를 마우스로 클릭하였습니다");

        // x와 y를 게임메니저에 전달 해야한다.
        GameManager.Instance.ProcessInput(x, y);
    }
}
