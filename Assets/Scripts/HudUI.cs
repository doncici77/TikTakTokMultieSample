using UnityEngine;

public class HudUI : MonoBehaviour
{
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    private void Start()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);

        GameManager.Instance.ChangeHudUI += ChangeTurnHud;
    }

    private void ChangeTurnHud(SquareState currentTurn) // ������ O, �������� X
    {
        if(currentTurn == SquareState.Circle)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else if (currentTurn == SquareState.Cross)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
    }
}
