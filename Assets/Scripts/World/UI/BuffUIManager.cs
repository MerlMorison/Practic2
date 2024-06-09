using System.Collections;
using TMPro;
using UnityEngine;

public class BuffUIManager : MonoBehaviour
{
    public TMP_Text buffCountText; // Ссылка на текстовое поле для отображения количества бафов
    public TMP_Text timeToNextBuffText; // Ссылка на текстовое поле для отображения времени до следующего бафа
    public TMP_Text buffTimerText; // Ссылка на текстовое поле для отображения времени действия баффа

    private int buffCount = 0; // Количество бафов
    [SerializeField] private float timeToNextBuff = 10f; // Время до следующего бафа
    private float buffTimer = 0f; // Таймер для отслеживания времени до следующего бафа
    [SerializeField] private float buffDuration = 5f; // Длительность баффа
    private float currentBuffTime = 0f; // Текущее время действия баффа
    public float BuffDuration { get { return buffDuration; } }


    private void Start()
    {
        StartCoroutine(UpdateBuffRoutine());
    }

    private IEnumerator UpdateBuffRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Подождать 1 секунду
            UpdateBuffTimer(); // Обновить таймер до следующего бафа
        }
    }

    private void UpdateBuffTimer()
    {
        buffTimer += 1f; // Увеличить таймер

        // Если прошло timeToNextBuff секунд, увеличиваем количество бафов и сбрасываем таймер
        if (buffTimer >= timeToNextBuff)
        {
            buffCount++;
            buffTimer = 0f;
        }

        // Обновляем текстовые поля UI для количества и времени до следующего баффа
        UpdateBuffCountUI();
        UpdateTimeToNextBuffUI();

        // Если текущее время баффа больше 0, обновляем текстовое поле UI для времени действия баффа
        if (currentBuffTime > 0)
        {
            currentBuffTime -= Time.deltaTime;
            UpdateBuffTimerUI();
        }
    }

    private void UpdateBuffCountUI()
    {
        buffCountText.text = "Buff Count: " + buffCount.ToString();
    }

    private void UpdateTimeToNextBuffUI()
    {
        timeToNextBuffText.text = "Time to Next Buff: " + (timeToNextBuff - buffTimer).ToString("F1") + "s";
    }

    private void UpdateBuffTimerUI()
    {
        buffTimerText.text = "Buff Timer: " + currentBuffTime.ToString("F1") + "s";
    }

    // Уменьшить количество баффов
    public void DecrementBuffCount()
    {
        if (buffCount > 0)
        {
            buffCount--;
            UpdateBuffCountUI();
        }
    }

    // Запустить таймер баффа
    public void StartBuffTimer(float duration)
    {
        currentBuffTime = duration;
        StartCoroutine(BuffTimerCoroutine());
    }


    private IEnumerator BuffTimerCoroutine()
    {
        while (currentBuffTime > 0f)
        {
            yield return null;
            currentBuffTime -= Time.deltaTime; // Уменьшить текущее время баффа
            UpdateBuffTimerUI(); // Обновить UI для времени действия баффа
        }
    }

    public int GetBuffCount()
    {
        return buffCount;
    }
}
