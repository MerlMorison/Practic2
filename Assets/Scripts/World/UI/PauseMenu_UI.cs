using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu_UI : MonoBehaviour
{
    public Action OnContinue;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonPressed);
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
        _settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        _exitButton.onClick.AddListener(OnExitButtonPressed);
    }
    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonPressed);
        _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonPressed);
        _exitButton.onClick.RemoveListener(OnExitButtonPressed);
    }

    public void OnContinueButtonPressed()
    {
        OnContinue?.Invoke();
    }
    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSettingsButtonPressed()
    {
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
