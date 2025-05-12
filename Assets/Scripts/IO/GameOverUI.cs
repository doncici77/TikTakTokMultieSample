using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;

    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameEnded += OnGameOverUI;
    }

    private void OnGameOverUI(GameOverState gameOverState)
    {
        textUI.text = $"{gameOverState}";
        gameObject.SetActive(true);
    }
}
