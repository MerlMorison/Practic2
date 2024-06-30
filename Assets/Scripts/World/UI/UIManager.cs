using System.Collections;
using InputS;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private UIControl _inputManager;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        _inputManager = new UIControl();
        _inputManager.UI.Pause.performed += PauseUI;
    }

    private void Start()
    {
        // Показать стартовое меню при запуске игры
        ShowStartMenu();
    }

    private void SetMenuState(GameObject menuToActivate)
    {
        startMenu.SetActive(menuToActivate == startMenu);
        gameOverMenu.SetActive(menuToActivate == gameOverMenu);
        gameMenu.SetActive(menuToActivate == gameMenu);
        pauseMenu.SetActive(menuToActivate == pauseMenu);
        settingsMenu.SetActive(menuToActivate == settingsMenu);
    }

    private void ShowStartMenu()
    {
        SetMenuState(startMenu);
    }

    public void StartGame()
    {
        // Начать корутину для смены меню с задержкой
        StartCoroutine(DelayedStartGame());
    }

    private IEnumerator DelayedStartGame()
    {
        yield return new WaitForSeconds(0.12f);

        // Скрыть стартовое меню и открыть игровое меню
        SetMenuState(gameMenu);
    }

    public void ShowSettingsMenu()
    {
        SetMenuState(settingsMenu);
    }

    public void ShowGameOverMenu()
    {
        SetMenuState(gameOverMenu);
    }

    public void RestartGame()
    {
        // Перезагрузка текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseUI(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool isPaused = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isPaused);
            gameMenu.SetActive(isPaused);
        }
    }

    private void OnEnable() => _inputManager.Enable();

    private void OnDisable() => _inputManager.Disable();
}
