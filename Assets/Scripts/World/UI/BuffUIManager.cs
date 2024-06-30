using System.Collections;
using TMPro;
using UnityEngine;

public class BuffUIManager : MonoBehaviour
{
    public TMP_Text buffCountText;
    public TMP_Text timeToNextBuffText;
    public TMP_Text buffTimerText;

    private int buffCount = 0;
    [SerializeField] private float timeToNextBuff = 10f;
    private float buffTimer = 0f;
    [SerializeField] private float buffDuration = 5f;
    private float currentBuffTime = 0f;
    public float BuffDuration { get { return buffDuration; } }


    private void Start()
    {
        StartCoroutine(UpdateBuffRoutine());
    }

    private IEnumerator UpdateBuffRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UpdateBuffTimer();
        }
    }

    private void UpdateBuffTimer()
    {
        buffTimer += 1f;


        if (buffTimer >= timeToNextBuff)
        {
            buffCount++;
            buffTimer = 0f;
        }


        UpdateBuffCountUI();
        UpdateTimeToNextBuffUI();


        if (currentBuffTime > 0)
        {
            currentBuffTime -= Time.deltaTime;
            UpdateBuffTimerUI();
        }
        else buffTimerText.text = " ";
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


    public void DecrementBuffCount()
    {
        if (buffCount > 0)
        {
            buffCount--;
            UpdateBuffCountUI();
        }
    }


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
            currentBuffTime -= Time.deltaTime;
            UpdateBuffTimerUI();
        }
    }

    public int GetBuffCount()
    {
        return buffCount;
    }
}
