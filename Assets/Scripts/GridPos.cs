using UnityEngine;

public class GridPos : MonoBehaviour
{
    private void OnMouseDown()
    {
        Logger.Info("콜라이더를 마우스로 클릭하였습니다");
        Logger.Warning("이건 조심해야돼");
        Logger.Error("이건 위험할지도?");
    }
}
