using UnityEngine;

// --- UI Manager ---
// Switches UI
// Handles buttons callbacks (direct clicks on buttons like "Play")

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Canvas References")]
    public GameObject mainMenu;
    public GameObject gameplayHUD;
    public GameObject shop;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMainMenu(bool value) => mainMenu.SetActive(value);
    public void ShowGameplayHUD(bool value) => gameplayHUD.SetActive(value);
    public void ShowShop(bool value) => shop.SetActive(value);

    // BUTTON CALLBACKS

    public void OnStartButton()
    {
        GameStateMachine.Instance.ChangeState(GameStateType.Gameplay);
    }

    public void OnNextRoundButton()
    {
        GameStateMachine.Instance.ChangeState(GameStateType.Gameplay);
    }
}