using UnityEngine;

public class GridPos : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    private void OnMouseDown()
    {

        // x�� y�� ���Ӹ޴����� ���� �ؾ��Ѵ�.
        //GameManager.Instance.PlayMarker(x, y);
        GameManager.Instance.PlayMarker(x, y);
    }
}
