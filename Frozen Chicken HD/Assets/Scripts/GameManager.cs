using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Satisfaction")]
    [SerializeField] public int maxSatisfaction;
    [SerializeField] int currentSatisfaction;
    [SerializeField] Slider satisfactionSlider;

    [Header("Countdown")]
    [SerializeField] float timeLimit;
    float currentTime;
    [SerializeField] Slider timerSlider;
    [SerializeField] Gradient timerGradient;
    [SerializeField] Image timerFIll;
    [SerializeField] Text txtTime;
    public int CurrentSatisfaction { get { return currentSatisfaction; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        satisfactionSlider.maxValue = maxSatisfaction;
        satisfactionSlider.value = currentSatisfaction;

        currentTime = timeLimit;
        timerSlider.maxValue = timeLimit;
        timerSlider.value = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    public void AdjustSatisfaction(int val)
    {
        currentSatisfaction += val;
        if (currentSatisfaction > maxSatisfaction)
            currentSatisfaction = maxSatisfaction;
        else if (currentSatisfaction < 0)
            currentSatisfaction = 0;

        satisfactionSlider.value = currentSatisfaction;
    }

    void CountDown()
    {
        currentTime -= Time.deltaTime;

        TimeSpan test = TimeSpan.FromSeconds(currentTime);
        txtTime.text = string.Format("{0:00}:{1:00}", (int)test.TotalMinutes, test.Seconds);
        timerSlider.value = currentTime;
        timerFIll.color = timerGradient.Evaluate(timerSlider.normalizedValue);
    }


}
