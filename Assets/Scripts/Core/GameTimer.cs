using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    [Header("Timer Settings")]
    [SerializeField] private float maxTime = 900f; // 15分
    [SerializeField] private float currentTime = 0f;

    public float CurrentTime => currentTime;
    public float MaxTime => maxTime;
    public bool IsFinished => currentTime >= maxTime;

    public event Action OnGameFinished;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (IsFinished) return;

        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = maxTime;
            OnGameFinished?.Invoke();
        }
    }

    public string GetTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}