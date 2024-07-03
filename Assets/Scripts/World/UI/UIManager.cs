using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static InputS.UIControl;

namespace InputS
{
    public class UIManager : MonoBehaviour, IUIActions
    {

        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private PauseMenu_UI pauseMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject emptyMenu;

        private void Awake()
        {
            InputController.SubscribeOnUIInput(this);
            pauseMenu.OnContinue += TogglePause;
        }

        private void Start()
        {
            ShowStartMenu();
        }

        private void SetMenuState(GameObject menuToActivate)
        {
            startMenu.SetActive(menuToActivate == startMenu);
            gameOverMenu.SetActive(menuToActivate == gameOverMenu);
            gameMenu.SetActive(menuToActivate == gameMenu);
            settingsMenu.SetActive(menuToActivate == settingsMenu);
            emptyMenu.SetActive(menuToActivate == emptyMenu);
        }

        private void ShowStartMenu()
        {
            SetMenuState(startMenu);
        }

        public void StartGame()
        {
            StartCoroutine(DelayedStartGame());
        }

        private IEnumerator DelayedStartGame()
        {
            yield return new WaitForSeconds(0.12f);

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            TogglePause();
        }
        private void TogglePause()
        {
            var isPauseActive = !pauseMenu.gameObject.activeSelf;
            pauseMenu.gameObject.SetActive(isPauseActive);

            if (isPauseActive)
            {
                InputController.DisablePlayerInput();
                SetMenuState(emptyMenu);
            }
            else
            {
                InputController.EnablePlayerInput();
                gameMenu.SetActive(gameMenu);
            }
        }

        private void OnEnable()
        {
            InputController.EnableUIInput();
        }

        private void OnDisable()
        {
            InputController.DisableUIInput();
        }
    }
}
