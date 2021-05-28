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

        satisfactionSlider.value = currentSatisfaction;
    }

    void CountDown()
    {
        currentTime -= Time.deltaTime;
        timerSlider.value = currentTime;
        timerFIll.color = timerGradient.Evaluate(timerSlider.normalizedValue);
    }
}
