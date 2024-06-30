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
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject settingsMenu;

        private void Awake()
        {
            // ������������� �� UI ����
            InputController.SubscribeOnUIInput(this);
        }

        private void Start()
        {
            // �������� ��������� ���� ��� ������� ����
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
            // ������ �������� ��� ����� ���� � ���������
            StartCoroutine(DelayedStartGame());
        }

        private IEnumerator DelayedStartGame()
        {
            yield return new WaitForSeconds(0.12f);

            // ������ ��������� ���� � ������� ������� ����
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
            // ������������ ������� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // ����� ��� ��������� �����, ��������������� ���������� IUIActions
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                bool isPaused = pauseMenu.activeSelf;
                pauseMenu.SetActive(!isPaused);
                gameMenu.SetActive(isPaused);
            }
        }

        // ��������� � ���������� �����
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
