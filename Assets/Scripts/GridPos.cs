using UnityEngine;

public class GridPos : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    private void OnMouseDown()
    {
        Logger.Info($"({x}, {y})��ǥ�� ���콺�� Ŭ���Ͽ����ϴ�");

        // x�� y�� ���Ӹ޴����� ���� �ؾ��Ѵ�.
        GameManager.Instance.ProcessInput(x, y);
    }
}
