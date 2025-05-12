using UnityEngine;

public class GridPos : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    private void OnMouseDown()
    {

        // x와 y를 게임메니저에 전달 해야한다.
        //GameManager.Instance.PlayMarker(x, y);
        GameManager.Instance.PlayMarker(x, y);
    }
}
